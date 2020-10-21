export class Account {
    id: number;
    name: string;
    zip: string;
    country: string;
    city: string;
    street: string;
    streetNo: string;
    accountType: string;
    contactEmail: string;
    adminEmails: string[];
    userEmails: string[];
}

export class CreateAccountCommand {
    name: string;
    zip: string;
    country: string;
    city: string;
    street: string;
    streetNo: string;
    accountType: string;
    contactEmail: string;
}

export class UpdateAccountCommand {
    id: number;
    name: string;
    zip: string;
    country: string;
    city: string;
    street: string;
    streetNo: string;
    accountType: string;
    contactEmail: string;
    adminEmails: string[];
    userEmails: string[];
}