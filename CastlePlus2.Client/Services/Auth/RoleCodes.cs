namespace CastlePlus2.Client.Services.Auth;

public static class RoleCodes
{
    public const string Admin = "Admin,ADMIN";
    public const string User = "User,USER";
    public const string Manager = "Manager,MANAGER";
    public const string Employee = "Employee,EMPLOYEE";

    public const string AdminOrManager = $"{Admin},{Manager}";
    public const string AdminOrEmployee = $"{Admin},{Employee}";
    public const string AdminOrManagerOrEmployee = $"{Admin},{Manager},{Employee}";
    public const string AdminOrEmployeeOrUser = $"{Admin},{Employee},{User}";
}