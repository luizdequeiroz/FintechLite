import { getJson, postJson } from "./http";

export type AccountResponse = {
    id: string;
    name: string;
    balanceCents: number;
    createdAtUtc: string;
};

export type CreateAccountRequest = {
    name: string;
}

export function listAccounts(): Promise<AccountResponse[]> {
    return getJson<AccountResponse[]>("/api/account");
}

export function createAccount(req: CreateAccountRequest): Promise<AccountResponse> {
    return postJson<AccountResponse, CreateAccountRequest>("/api/account", req);
}