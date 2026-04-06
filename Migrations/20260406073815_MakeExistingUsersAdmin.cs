using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emlak_son.Migrations
{
    /// <inheritdoc />
    public partial class MakeExistingUsersAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO AspNetUserRoles (UserId, RoleId)
                SELECT u.Id, r.Id 
                FROM AspNetUsers u, AspNetRoles r
                WHERE r.NormalizedName = 'ADMIN'
                AND NOT EXISTS (
                    SELECT 1 
                    FROM AspNetUserRoles ur 
                    WHERE ur.UserId = u.Id AND ur.RoleId = r.Id
                )
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE ur
                FROM AspNetUserRoles ur
                INNER JOIN AspNetRoles r ON ur.RoleId = r.Id
                WHERE r.NormalizedName = 'ADMIN'
            ");
        }
    }
}
