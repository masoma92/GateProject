export class UserGates {
    email: string;
    accessRight: boolean;
    adminRight: boolean;
}

export class Gate {
    id: number;
    name: string;
    gateTypeName: string;
    serviceId: string;
    characteristicId: string;
    accountName: string;
    users: UserGates[];
}

export class CreateGateCommand {
    name: string;
    gateTypeName: string;
    accountName: string;
}