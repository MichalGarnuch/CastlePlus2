namespace CastlePlus2.Shared.Auth;

public static class RoleCodes
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Manager = "Manager";
    public const string Employee = "Employee";

    public const string AdminOrManager = Admin + "," + Manager;
    public const string AdminOrEmployee = Admin + "," + Employee;
    public const string AdminOrManagerOrEmployee = Admin + "," + Manager + "," + Employee;
    public const string AdminOrEmployeeOrUser = Admin + "," + Employee + "," + User;
}
