import React, { useState, useEffect } from 'react';
import { leadsApi } from './services/api';
import TabNav from './components/TabNav';
import LeadCard from './components/LeadCard';
import Toast from './components/Toast';
import AddLeadModal from './components/AddLeadModal';
import { Loader2, Sparkles, Plus } from 'lucide-react';

function App() {
  const [activeTab, setActiveTab] = useState('invited');
  const [leads, setLeads] = useState([]);
  const [loading, setLoading] = useState(true);
  const [toast, setToast] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const showToast = (message, type = 'success') => {
    setToast({ message, type });
  };

  const fetchLeads = async () => {
    try {
      setLoading(true);
      const status = activeTab === 'invited' ? 'New' : 'Accepted';
      const data = await leadsApi.getLeadsByStatus(status);
      setLeads(data);
    } catch (error) {
      console.error('Error fetching leads:', error);
      showToast('Error loading leads. Check if the API is running.', 'error');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchLeads();
  }, [activeTab]);

  const handleAccept = async (leadId) => {
    try {
      await leadsApi.acceptLead(leadId);
      showToast('Lead accepted successfully!', 'success');
      await fetchLeads();
    } catch (error) {
      console.error('Error accepting lead:', error);
      const errorMessage = error.response?.data?.message || 'Error accepting lead. Try again.';
      showToast(errorMessage, 'error');
    }
  };

  const handleDecline = async (leadId) => {
    try {
      await leadsApi.declineLead(leadId);
      showToast('Lead declined successfully!', 'success');
      await fetchLeads();
    } catch (error) {
      console.error('Error declining lead:', error);
      const errorMessage = error.response?.data?.message || 'Error declining lead. Try again.';
      showToast(errorMessage, 'error');
    }
  };

  const handleCreateLead = async (leadData) => {
    try {
      await leadsApi.createLead(leadData);
      showToast('Lead created successfully!', 'success');
      if (activeTab === 'invited') {
        await fetchLeads();
      }
    } catch (error) {
      console.error('Error creating lead:', error);
      const errorMessage = error.response?.data?.message || 'Error creating lead. Check the data and try again.';
      showToast(errorMessage, 'error');
    }
  };

  return (
    <div className="min-h-screen bg-gray-100">
      {toast && (
        <Toast
          message={toast.message}
          type={toast.type}
          onClose={() => setToast(null)}
        />
      )}

      <AddLeadModal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onSubmit={handleCreateLead}
      />

      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <div className="mb-8 bg-slate-700 rounded-xl p-8 shadow-lg">
          <div className="flex items-center gap-3">
            <div className="p-3 bg-slate-600 rounded-lg">
              <Sparkles className="w-8 h-8 text-white" />
            </div>
            <div>
              <h1 className="text-3xl font-bold text-white">Gerenciador de Leads</h1>
              <p className="mt-2 text-slate-200 text-base">Gerencie seus leads com eficiência e profissionalismo</p>
            </div>
          </div>
        </div>

        <div className="bg-white rounded-xl shadow-lg overflow-hidden border border-gray-200">
          <TabNav activeTab={activeTab} setActiveTab={setActiveTab} />
          
          <div className="p-8 bg-white">
            {loading ? (
              <div className="flex flex-col justify-center items-center py-16">
                <Loader2 className="w-12 h-12 animate-spin text-slate-600 mb-4" />
                <p className="text-gray-500 font-medium">Loading leads...</p>
              </div>
            ) : leads.length === 0 ? (
              <div className="text-center py-16 bg-slate-50 rounded-lg border-2 border-dashed border-slate-200">
                <div className="inline-flex items-center justify-center w-16 h-16 bg-slate-100 rounded-full mb-4">
                  <Sparkles className="w-8 h-8 text-slate-500" />
                </div>
                <p className="text-gray-700 text-xl font-semibold">No leads found</p>
                <p className="text-gray-500 mt-2">Leads will appear here when created</p>
              </div>
            ) : (
              <div className="space-y-4">
                {leads.map((lead) => (
                  <LeadCard
                    key={lead.id}
                    lead={lead}
                    onAccept={handleAccept}
                    onDecline={handleDecline}
                    showActions={activeTab === 'invited'}
                  />
                ))}
              </div>
            )}
          </div>
        </div>

        <button
          onClick={() => setIsModalOpen(true)}
          className="fixed bottom-8 right-8 flex items-center gap-2 px-6 py-4 bg-teal-500 text-white rounded-full hover:bg-teal-600 transition-all duration-300 font-semibold shadow-2xl hover:shadow-3xl transform hover:scale-110 z-40"
          title="Add new lead"
        >
          <Plus className="w-6 h-6" />
          <span className="hidden sm:inline">New Lead</span>
        </button>
      </div>
    </div>
  );
}

export default App;
