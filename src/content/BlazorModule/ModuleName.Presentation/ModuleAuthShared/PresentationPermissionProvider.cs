//using ModuleAuth.Shared.Contracts;

//namespace ModuleName.Presentation.ModuleAuthShared;

//internal class PresentationPermissionProvider : IPermissionProvider
//{
//    public static readonly PermissionInfo ListPersonsPermission = new("ListPersonsPermissionKey", "ListPersonsPermission Name", "ListPersonsPermission Description");
//    public static readonly PermissionInfo DisplayPersonPermission = new("DisplayPersonPermissionKey", "DisplayPersonPermission Name", "DisplayPersonPermission Description");
//    public static readonly PermissionInfo CreatePersonPermission = new("CreatePersonPermissionKey", "CreatePersonPermission Name", "CreatePersonPermission Description");
//    public static readonly PermissionInfo UpdatePersonPermission = new("UpdatePersonPermissionKey", "UpdatePersonPermission Name", "UpdatePersonPermission Description");
//    public static readonly PermissionInfo DeletePersonPermission = new("DeletePersonPermissionKey", "DeletePersonPermission Name", "DeletePersonPermission Description");
//    public static readonly PermissionInfo DiplayPersonContactPermission = new("DiplayPersonContactPermissionKey", "DiplayPersonContactPermission Name", "DiplayPersonContactPermission Description");
//    public static readonly PermissionInfo CreatePersonContactPermission = new("CreatePersonContactPermissionKey", "CreatePersonContactPermission Name", "CreatePersonContactPermission Description");
//    public static readonly PermissionInfo UpdatePersonContactPermission = new("UpdatePersonContactPermissionKey", "UpdatePersonContactPermission Name", "UpdatePersonContactPermission Description");
//    public static readonly PermissionInfo DeletePersonContactPermission = new("DeletePersonContactPermissionKey", "DeletePersonContactPermission Name", "DeletePersonContactPermission Description");


//    public IEnumerable<PermissionInfo> GetPermissions()
//    {
//        yield return ListPersonsPermission;
//        yield return DisplayPersonPermission;
//        yield return CreatePersonPermission;
//        yield return UpdatePersonPermission;
//        yield return DeletePersonPermission;
//        yield return DiplayPersonContactPermission;
//        yield return CreatePersonContactPermission;
//        yield return UpdatePersonContactPermission;
//        yield return DeletePersonContactPermission;
//    }
//}