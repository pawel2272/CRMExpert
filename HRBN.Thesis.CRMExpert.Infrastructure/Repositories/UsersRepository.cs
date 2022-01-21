using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HRBN.Thesis.CRMExpert.Domain;
using HRBN.Thesis.CRMExpert.Domain.Core.Entities;
using HRBN.Thesis.CRMExpert.Domain.Core.Enums;
using HRBN.Thesis.CRMExpert.Domain.Core.Pagination;
using HRBN.Thesis.CRMExpert.Domain.Core.Repositories;
using HRBN.Thesis.CRMExpert.Infrastructure.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HRBN.Thesis.CRMExpert.Infrastructure.Repositories
{
    public sealed class UsersRepository : IUsersRepository
    {
        private readonly CRMContext _dbContext;
        private readonly IPasswordHasher<User> _hasher;
        private readonly JwtOptions _jwtOptions;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITokenRepository _tokenRepository;

        public UsersRepository(CRMContext dbContext, IPasswordHasher<User> hasher, JwtOptions jwtOptions,
            IHttpContextAccessor contextAccessor, ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
            _contextAccessor = contextAccessor;
            _jwtOptions = jwtOptions;
            _hasher = hasher;
            _dbContext = dbContext;
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
            return user;
        }

        public async Task DeleteAsync(User entity)
        {
            await Task.Factory.StartNew(() => { _dbContext.Users.Remove(entity); });
        }

        public async Task<IPageResult<User>> SearchAsync(string searchPhrase, int pageNumber, int pageSize,
            string orderBy, SortDirection sortDirection)
        {
            string lowerCaseSearchPhrase = searchPhrase?.ToLower();
            
            var baseQuery = _dbContext.Users
                .Where(e => searchPhrase == null ||
                            (e.Id.ToString().Contains(lowerCaseSearchPhrase)
                             || e.Username.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.Gender.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.FirstName.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.LastName.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.Phone.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.Email.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.Street.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.PostalCode.ToLower().Contains(lowerCaseSearchPhrase)
                             || e.City.ToLower().Contains(lowerCaseSearchPhrase)
                            ));
            if (!string.IsNullOrEmpty(orderBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<User, object>>>()
                {
                    {nameof(User.Username), e => e.Username},
                    {nameof(User.FirstName), e => e.FirstName},
                    {nameof(User.LastName), e => e.LastName},
                    {nameof(User.Phone), e => e.Phone},
                    {nameof(User.Email), e => e.Email},
                    {nameof(User.Street), e => e.Street},
                    {nameof(User.PostalCode), e => e.PostalCode},
                    {nameof(User.City), e => e.City}
                };

                Expression<Func<User, object>> selectedColumn;

                if (columnSelectors.Keys.Contains(orderBy))
                {
                    selectedColumn = columnSelectors[orderBy];
                }
                else
                {
                    selectedColumn = columnSelectors["Username"];
                }

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var entities = await baseQuery.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<User>(entities, baseQuery.Count(), pageSize, pageNumber);
        }

        public async Task AddAsync(User entity)
        {
            entity.Password = _hasher.HashPassword(entity, entity.Password);
            await _dbContext.Users.AddAsync(entity);
        }

        public async Task UpdateAsync(User entity)
        {
            await Task.Factory.StartNew(() =>
            {
                entity.Password = _hasher.HashPassword(entity, entity.Password);
                _dbContext.Users.Update(entity);
            });
        }

        private CookieBuilder CreateAuthorizationCookie(double time)
        {
            CookieBuilder cookie = new CookieBuilder()
            {
                IsEssential = true,
                Expiration = TimeSpan.FromMinutes(time),
                Name = "Authorization"
            };
            return cookie;
        }

        private JsonWebToken CreateToken(User entity)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, entity.Username),
                new Claim(ClaimTypes.Email, entity.Email),
                new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString())
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.JwtKey));
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.Now.AddMinutes(_jwtOptions.JwtExpireMinutes);
            JwtSecurityToken token = new JwtSecurityToken(_jwtOptions.JwtIssuer,
                _jwtOptions.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: signingCredentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string tokenString = handler.WriteToken(token);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long) (new TimeSpan(expires.ToUniversalTime().Ticks - centuryBegin.Ticks).TotalSeconds);
            return new JsonWebToken()
            {
                AccessToken = tokenString,
                Expires = exp
            };
        }

        public async Task<string> LoginAsync(string username, string password, bool rememberMe)
        {
            var userToBeVerified = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(username));
            if (userToBeVerified == null)
            {
                return null;
            }

            var result = _hasher.VerifyHashedPassword(userToBeVerified, userToBeVerified.Password, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var cookie = rememberMe
                ? CreateAuthorizationCookie(_jwtOptions.JwtExpireMinutes * 10)
                : CreateAuthorizationCookie(_jwtOptions.JwtExpireMinutes);
            var token = CreateToken(userToBeVerified);
            _contextAccessor.HttpContext.Response.Cookies.Append("Authorization", token.AccessToken,
                cookie.Build(_contextAccessor.HttpContext));
            return token.AccessToken;
        }

        public async Task LogoutAsync()
        {
            await _tokenRepository.DeactivateCurrentAsync();
            _contextAccessor.HttpContext.Response.Cookies.Delete("Authorization");
            CookieBuilder cookie = CreateAuthorizationCookie(-60);
            _contextAccessor.HttpContext.Response.Cookies.Append("Authorization", "",
                cookie.Build(_contextAccessor.HttpContext));
        }
    }
}