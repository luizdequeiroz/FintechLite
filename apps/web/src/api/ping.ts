import { getJson } from "./http";

export type PingResponse = {
    name: string;
    environment: string;
    version: string;
    utc: string;
}

export async function getPing(): Promise<PingResponse> {
    return getJson<PingResponse>("/api/ping");
}