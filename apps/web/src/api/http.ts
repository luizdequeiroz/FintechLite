export class HttpError extends Error {
    public status: number;
    public body?: string;

    constructor(
        message: string,
        status: number,
        body?: string
    ) {
        super(message);

        this.status = status;
        this.body = body;
    }
}

async function request(path: string, init?: RequestInit): Promise<Response> {
    // Usando path relativo ("/api/..." e "/health") pra funcionar com proxy no dev
    // e em prod quando estiver no mesmo domínio (ou atrás de um gateway).
    const controller = new AbortController();
    const timeout = window.setTimeout(() => controller.abort(), 8000);

    try {
        return await fetch(path, { ...init, signal: controller.signal });
    } finally {
        window.clearTimeout(timeout);
    }
}

export async function getText(path: string): Promise<string> {
    const res = await request(path);
    const text = await res.text();

    if (!res.ok) throw new HttpError(`GET ${path} failed`, res.status, text);
    return text;
}

export async function getJson<T>(path: string): Promise<T> {
    const res = await request(path);
    const text = await res.text();

    if (!res.ok) throw new HttpError(`GET ${path} failed`, res.status, text);
    return JSON.parse(text) as T;
}
