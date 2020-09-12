using K1vs.DotChat.FrameworkUtils.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace K1vs.DotChat.Exceptions
{
    public enum ExceptionCode
    {
        [NameDescription("UnknownException", "An unknown error has occurred.")]
        Unknown = 0,

        [NameDescription("AccessDeniedException", "Access to the requested resource is denied.")]
        AccessDenied = 1000,
        [NameDescription("AccessDeniedDueToSelfElevationException", "The user cannot independently expand his access rights.")]
        AccessDeniedDueToSelfElevation = AccessDenied + 10,
        [NameDescription("AccessDeniedDueToAdvancedProtectionException", "Access to the requested resource is denied. There are additional restrictions.")]
        AccessDeniedDueToAdvancedProtection = AccessDenied + 20,
        [NameDescription("AccessDeniedForeignException", "An attempt to access foreign's information is suppressed.")]
        AccessDeniedForeign = AccessDenied + 30,
        [NameDescription("AccessDeniedUndefinedPermissionsException", "Failed to get information about the permissions of the current user.")]
        AccessDeniedUndefinedPermissions = AccessDenied + 40,
        [NameDescription("AccessDeniedSelfStatusException", "User status does not allow the requested operation.")]
        AccessDeniedSelfStatus = AccessDenied + 50,
        [NameDescription("AccessDeniedNotEnoughPermissionsException", "The user does not have sufficient permissions to perform the action.")]
        AccessDeniedNotEnoughPermissions = AccessDenied + 60,

        [NameDescription("RulesViolationException", "The action violates the application rules.")]
        RulesViolation = 2000,
        [NameDescription("RulesViolationEditImmutableException", "It is forbidden to change Immutable values.")]
        RulesViolationEditImmutable = RulesViolation + 10,

        [NameDescription("StorageFaultException", "An error occurred while working with the storage.")]
        StorageFault = 3000,
        [NameDescription("StorageFaultItemNotFoundException", "The requested required entity was not found in the storage.")]
        StorageFaultItemNotFound = StorageFault * 1000,

        [NameDescription("ValidationFaultException", "Provided data did not pass validation.")]
        ValidationFault = 4000,
    }
}
