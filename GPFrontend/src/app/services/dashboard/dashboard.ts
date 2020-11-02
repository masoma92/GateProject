export class CreateAccountChart {
    constructor(public from: Date, public to: Date){};
}

export interface ChartResponse {
    chartData: {[key: string]: string};
}

export interface KeyValuePair {
    key: any;
    value: any;
}

export class GetEnters {
    constructor(public from: Date, public to: Date, public accountId: number) {};
}

export interface GetEntersResponse {
    name: string;
    email: string;
    firstUse: Date;
    lastUse: Date;
    gateName: string;
}