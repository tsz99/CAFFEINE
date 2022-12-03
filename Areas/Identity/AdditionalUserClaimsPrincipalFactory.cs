using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CAFFEINE.Areas.Identity
{
    public class AdditionalUserClaimsPrincipalFactory :
        UserClaimsPrincipalFactory<IdentityUser>
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(IdentityUser user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            var userRoles = await userManager.GetRolesAsync(user);
            if (!userRoles.Contains("Member"))
            {
                await userManager.AddToRoleAsync(user, "Member");
            }
            var claims = new List<Claim>();

            if (user.TwoFactorEnabled)
            {
                claims.Add(new Claim("amr", "mfa"));
            }
            else
            {
                claims.Add(new Claim("amr", "pwd"));
            }

            
            identity.AddClaims(claims);
            return principal;
        }
    }
}
