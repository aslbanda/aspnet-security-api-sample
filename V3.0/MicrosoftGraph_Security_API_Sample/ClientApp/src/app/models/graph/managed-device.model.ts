export class ManagedDevice {
    serialNumber: string;
    deviceName: string;
    operatingSystem: string;
    osVersion: string;
    deviceEnrollmentType: string;
    deviceCategoryDisplayName: string;
    usersLoggedOn: UsersLoggedOn;
    managementAgent: string;
    deviceType: string;
    userPrincipalName: string;
    lastSyncDateTime: Date;
    userDisplayName: string;
}

class UsersLoggedOn {
    userId: string;
    lastLogOnDateTime: Date;
}