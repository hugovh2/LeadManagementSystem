import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5000/api';

const apiClient = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const leadsApi = {
  getAllLeads: async () => {
    const response = await apiClient.get('/leads');
    return response.data;
  },

  getLeadsByStatus: async (status) => {
    const response = await apiClient.get(`/leads/status/${status}`);
    return response.data;
  },

  createLead: async (leadData) => {
    const response = await apiClient.post('/leads', leadData);
    return response.data;
  },

  acceptLead: async (leadId) => {
    const response = await apiClient.put(`/leads/${leadId}/accept`);
    return response.data;
  },

  declineLead: async (leadId) => {
    const response = await apiClient.put(`/leads/${leadId}/decline`);
    return response.data;
  },
};
