import { useState, useEffect, FormEvent } from "react";
import axios from "axios";
import { Subscriber, NewSubscriber } from "./types/Subscriber";

const API_BASE = "/api/subscribers";

function App() {
  const [subscribers, setSubscribers] = useState<Subscriber[]>([]);
  const [form, setForm] = useState<NewSubscriber>({ name: "", email: "" });
  const [loading, setLoading] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const fetchSubscribers = async () => {
    setLoading(true);
    setError(null);
    try {
      const { data } = await axios.get<Subscriber[]>(API_BASE);
      setSubscribers(data);
    } catch {
      setError("Failed to load subscribers. Is the API running?");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchSubscribers();
  }, []);

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setSubmitting(true);
    setError(null);
    setSuccess(null);
    try {
      await axios.post(API_BASE, form);
      setSuccess(`${form.name} has been subscribed!`);
      setForm({ name: "", email: "" });
      await fetchSubscribers();
    } catch (err) {
      if (axios.isAxiosError(err) && err.response?.data?.message) {
        setError(err.response.data.message);
      } else {
        setError("Failed to add subscriber. Please try again.");
      }
    } finally {
      setSubmitting(false);
    }
  };

  const handleDelete = async (id: number, name: string) => {
    if (!window.confirm(`Remove ${name} from subscribers?`)) return;
    setError(null);
    setSuccess(null);
    try {
      await axios.delete(`${API_BASE}/${id}`);
      setSuccess(`${name} has been removed.`);
      setSubscribers((prev) => prev.filter((s) => s.id !== id));
    } catch {
      setError("Failed to delete subscriber. Please try again.");
    }
  };

  const formatDate = (iso: string) =>
    new Date(iso).toLocaleDateString("en-US", {
      year: "numeric",
      month: "short",
      day: "numeric",
    });

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Header */}
      <header className="bg-indigo-700 shadow">
        <div className="max-w-4xl mx-auto px-4 py-6">
          <h1 className="text-3xl font-bold text-white tracking-tight">
            SubscribeApp
          </h1>
          <p className="mt-1 text-indigo-200 text-sm">
            Manage your email subscribers
          </p>
        </div>
      </header>

      <main className="max-w-4xl mx-auto px-4 py-8 space-y-8">
        {/* Alerts */}
        {error && (
          <div className="rounded-md bg-red-50 border border-red-200 p-4 text-red-700 text-sm">
            {error}
          </div>
        )}
        {success && (
          <div className="rounded-md bg-green-50 border border-green-200 p-4 text-green-700 text-sm">
            {success}
          </div>
        )}

        {/* Add Subscriber Form */}
        <section className="bg-white rounded-xl shadow p-6">
          <h2 className="text-lg font-semibold text-gray-800 mb-4">
            Add Subscriber
          </h2>
          <form onSubmit={handleSubmit} className="flex flex-col sm:flex-row gap-3">
            <input
              type="text"
              placeholder="Full name"
              value={form.name}
              onChange={(e) => setForm({ ...form, name: e.target.value })}
              required
              className="flex-1 border border-gray-300 rounded-lg px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
            />
            <input
              type="email"
              placeholder="Email address"
              value={form.email}
              onChange={(e) => setForm({ ...form, email: e.target.value })}
              required
              className="flex-1 border border-gray-300 rounded-lg px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
            />
            <button
              type="submit"
              disabled={submitting}
              className="bg-indigo-600 hover:bg-indigo-700 disabled:bg-indigo-300 text-white font-medium rounded-lg px-6 py-2 text-sm transition-colors"
            >
              {submitting ? "Adding..." : "Subscribe"}
            </button>
          </form>
        </section>

        {/* Subscriber List */}
        <section className="bg-white rounded-xl shadow">
          <div className="px-6 py-4 border-b border-gray-100 flex items-center justify-between">
            <h2 className="text-lg font-semibold text-gray-800">
              Subscribers
            </h2>
            <span className="text-sm text-gray-500">
              {subscribers.length} total
            </span>
          </div>

          {loading ? (
            <div className="px-6 py-12 text-center text-gray-400 text-sm">
              Loading subscribers...
            </div>
          ) : subscribers.length === 0 ? (
            <div className="px-6 py-12 text-center text-gray-400 text-sm">
              No subscribers yet. Add one above.
            </div>
          ) : (
            <ul className="divide-y divide-gray-50">
              {subscribers.map((sub) => (
                <li
                  key={sub.id}
                  className="px-6 py-4 flex items-center justify-between hover:bg-gray-50 transition-colors"
                >
                  <div>
                    <p className="text-sm font-medium text-gray-800">
                      {sub.name}
                    </p>
                    <p className="text-sm text-gray-500">{sub.email}</p>
                    <p className="text-xs text-gray-400 mt-0.5">
                      Subscribed {formatDate(sub.createdAt)}
                    </p>
                  </div>
                  <button
                    onClick={() => handleDelete(sub.id, sub.name)}
                    className="text-red-500 hover:text-red-700 text-sm font-medium transition-colors ml-4"
                  >
                    Remove
                  </button>
                </li>
              ))}
            </ul>
          )}
        </section>
      </main>
    </div>
  );
}

export default App;
