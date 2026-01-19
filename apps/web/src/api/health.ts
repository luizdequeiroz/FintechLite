import { getText } from "./http";

export type HealthResponse = {
  status: string;
};

export async function getHealth(): Promise<string> {
  return getText("/health");
}
