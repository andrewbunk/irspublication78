export type Organization = {
    id: number;
    ein: string;
    name: string;
    city: string;
    state: string;
    country: string;
    deductibilityCodes: DeductibilityCode[];
}

export type DeductibilityCode = {
    id: number;
    code: string;
    orgType: string;
    deductibilityLimitation: string;
}

export type SearchAudit = {
    id: number;
    searchText: string;
    totalRecords: number;
}