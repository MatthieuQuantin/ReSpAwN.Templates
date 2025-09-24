//using ModuleAuth.Shared.Contracts;

//namespace ModuleName.Application.ModuleAuthShared;

//internal class ApplicationPermissionProvider : IPermissionProvider
//{
//    public static readonly PermissionInfo CreatePersonPermission = new("CreatePersonPermissionKey", "CreatePersonPermission Name", "CreatePersonPermission Description");
//    public static readonly PermissionInfo CreatePersonContactPermission = new("CreatePersonContactPermissionKey", "CreatePersonContactPermission Name", "CreatePersonContactPermission Description");
//    public static readonly PermissionInfo DeletePersonPermission = new("DeletePersonPermissionKey", "DeletePersonPermission Name", "DeletePersonPermission Description");
//    public static readonly PermissionInfo DeletePersonContactPermission = new("DeletePersonContactPermissionKey", "DeletePersonContactPermission Name", "DeletePersonContactPermission Description");
//    public static readonly PermissionInfo GetPersonByIdPermission = new("GetPersonByIdPermissionKey", "GetPersonByIdPermission Name", "GetPersonByIdPermission Description");
//    public static readonly PermissionInfo ListPersonsPermission = new("ListPersonsPermissionKey", "ListPersonsPermission Name", "ListPersonsPermission Description");
//    public static readonly PermissionInfo UpdatePersonPermission = new("UpdatePersonPermissionKey", "UpdatePersonPermission Name", "UpdatePersonPermission Description");
//    public static readonly PermissionInfo UpdatePersonContactPermission = new("UpdatePersonContactPermissionKey", "UpdatePersonContactPermission Name", "UpdatePersonContactPermission Description");


//    public IEnumerable<PermissionInfo> GetPermissions()
//    {
//        yield return CreatePersonPermission;
//        yield return CreatePersonContactPermission;
//        yield return DeletePersonPermission;
//        yield return DeletePersonContactPermission;
//        yield return GetPersonByIdPermission;
//        yield return ListPersonsPermission;
//        yield return UpdatePersonPermission;
//        yield return UpdatePersonContactPermission;
//    }
//}