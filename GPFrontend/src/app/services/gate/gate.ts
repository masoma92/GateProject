export class UserGate {
    email: string;
    accessRight: boolean = false;
    adminRight: boolean = false;
}

export class Gate {
    id: number;
    name: string;
    gateTypeName: string;
    serviceId: string;
    characteristicId: string;
    accountName: string;
    requestAdminAccess: boolean;
    users: UserGate[];
}

export class CreateGateCommand {
    name: string;
    gateTypeName: string;
    accountName: string;
}