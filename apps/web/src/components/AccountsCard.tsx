import { useEffect, useMemo, useState } from "react";
import { createAccount, listAccounts, type AccountResponse } from "../api/accounts";

type State =
  | { kind: "idle" | "loading" }
  | { kind: "ready"; items: AccountResponse[] }
  | { kind: "error"; message: string };

function AccountsCard() {
  const [state, setState] = useState<State>({ kind: "idle" });
  const [name, setName] = useState("");
  const [busy, setBusy] = useState(false);

  const total = useMemo(() => {
    if (state.kind !== "ready") return 0;
    return state.items.reduce((sum, a) => sum + a.balanceCents, 0);
  }, [state]);

  async function refresh() {
    setState({ kind: "loading" });
    try {
      const items = await listAccounts();
      setState({ kind: "ready", items });
    } catch (e) {
      const msg = e instanceof Error ? e.message : String(e);
      setState({ kind: "error", message: msg });
    }
  }

  async function onCreate() {
    const trimmed = name.trim();
    if (!trimmed) return;

    setBusy(true);
    try {
      await createAccount({ name: trimmed });
      setName("");
      await refresh();
    } catch (e) {
      const msg = e instanceof Error ? e.message : String(e);
      setState({ kind: "error", message: msg });
    } finally {
      setBusy(false);
    }
  }

  useEffect(() => {
    void refresh();
  }, []);

  return (
    <div className="card" style={{ marginTop: 24 }}>
      <h2>Accounts</h2>

      <div style={{ display: "flex", gap: 8, marginTop: 12 }}>
        <input
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Account name (ex: Main)"
          style={{ flex: 1, padding: "8px 10px" }}
        />
        <button onClick={() => void onCreate()} disabled={busy || !name.trim()}>
          {busy ? "Creating..." : "Create"}
        </button>
        <button onClick={() => void refresh()} disabled={state.kind === "loading"}>
          Refresh
        </button>
      </div>

      {state.kind === "loading" && <p style={{ marginTop: 12 }}>Loading...</p>}

      {state.kind === "error" && (
        <p style={{ marginTop: 12, color: "#ef4444" }}>
          <b>Error:</b> {state.message}
        </p>
      )}

      {state.kind === "ready" && (
        <>
          <p style={{ marginTop: 12, opacity: 0.8 }}>
            Total balance (cents): <b>{total}</b>
          </p>

          {state.items.length === 0 ? (
            <p style={{ marginTop: 12 }}>No accounts yet.</p>
          ) : (
            <ul style={{ marginTop: 12, paddingLeft: 18 }}>
              {state.items.map((a) => (
                <li key={a.id} style={{ marginBottom: 6 }}>
                  <b>{a.name}</b> — {a.balanceCents} cents{" "}
                  <span style={{ opacity: 0.7 }}>
                    ({new Date(a.createdAtUtc).toLocaleString()})
                  </span>
                </li>
              ))}
            </ul>
          )}
        </>
      )}
    </div>
  );
}

export default AccountsCard