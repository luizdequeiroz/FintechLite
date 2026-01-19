import { useEffect, useState } from "react";
import { getPing, type PingResponse } from "../api/ping";

type State =
  | { status: "idle" | "loading"; data?: PingResponse; error?: undefined }
  | { status: "success"; data: PingResponse; error?: undefined }
  | { status: "error"; data?: undefined; error: string };

export function PingCard() {
  const [state, setState] = useState<State>({ status: "idle" });
  const [lastChecked, setLastChecked] = useState<Date | null>(null);

  async function run() {
    setState({ status: "loading" });
    try {
      const data = await getPing();
      setState({ status: "success", data });
      setLastChecked(new Date());
    } catch (e) {
      const msg = e instanceof Error ? e.message : String(e);
      setState({ status: "error", error: msg });
      setLastChecked(new Date());
    }
  }

  useEffect(() => {
    run();
  }, []);

  const isOk = state.status === "success";

  return (
    <div className="card">
      <h2 style={{ display: "flex", alignItems: "center", gap: 8 }}>
        API Ping{" "}
        <span
          title={isOk ? "OK" : "Erro"}
          style={{
            width: 10,
            height: 10,
            borderRadius: 999,
            display: "inline-block",
            background: isOk ? "#22c55e" : "#ef4444",
          }}
        />
      </h2>

      <div style={{ marginTop: 8, fontSize: 14 }}>
        <div>
          <b>URL:</b> <code>/api/ping</code>
        </div>

        {state.status === "loading" && <div style={{ marginTop: 8 }}>Carregando…</div>}

        {state.status === "error" && (
          <div style={{ marginTop: 8, color: "#ef4444" }}>
            <b>Error:</b> {state.error}
          </div>
        )}

        {state.status === "success" && (
          <div style={{ marginTop: 8, lineHeight: 1.6 }}>
            <div>
              <b>Name:</b> {state.data.name}
            </div>
            <div>
              <b>Environment:</b> {state.data.environment}
            </div>
            <div>
              <b>Version:</b> {state.data.version}
            </div>
            <div>
              <b>UTC:</b> {state.data.utc}
            </div>
          </div>
        )}

        <div style={{ marginTop: 12, color: "#6b7280", fontSize: 12 }}>
          <b>Last checked:</b>{" "}
          {lastChecked ? lastChecked.toLocaleString() : "—"}
        </div>

        <button style={{ marginTop: 12 }} onClick={run} disabled={state.status === "loading"}>
          Re-check
        </button>
      </div>
    </div>
  );
}
