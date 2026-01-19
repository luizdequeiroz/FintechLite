import { PingCard } from "./components/PingCard";
import './App.css'
import HealthCard from "./components/HealthCard";

function App() {

  return (
    <div style={{ maxWidth: 720, margin: '0 auto', padding: 24 }}>
      <HealthCard />
      <PingCard />
    </div>
  )
}

export default App
