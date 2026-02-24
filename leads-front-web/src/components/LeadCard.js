import React from 'react';
import { MapPin, Tag, Calendar, Mail, Phone, Briefcase, CheckCircle2, XCircle } from 'lucide-react';

const LeadCard = ({ lead, onAccept, onDecline, showActions }) => {
  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
      month: 'long',
      day: 'numeric',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    });
  };

  const getInitials = (firstName) => {
    return firstName.charAt(0).toUpperCase();
  };

  const getAvatarGradient = (firstName) => {
    const gradients = [
      'bg-gradient-to-br from-slate-500 to-slate-600',
      'bg-gradient-to-br from-blue-400 to-blue-500',
      'bg-gradient-to-br from-teal-400 to-teal-500',
      'bg-gradient-to-br from-cyan-400 to-cyan-500',
      'bg-gradient-to-br from-indigo-400 to-indigo-500',
      'bg-gradient-to-br from-gray-500 to-gray-600'
    ];
    const index = firstName.charCodeAt(0) % gradients.length;
    return gradients[index];
  };

  return (
    <div className="bg-white border border-gray-200 rounded-xl p-6 hover:shadow-lg hover:border-slate-300 transition-all duration-300 hover:-translate-y-1">
      <div className="flex items-start gap-4">
        <div className={`w-14 h-14 rounded-xl ${getAvatarGradient(lead.contactFirstName)} flex items-center justify-center text-white font-bold text-xl flex-shrink-0 shadow-lg`}>
          {getInitials(lead.contactFirstName)}
        </div>

        <div className="flex-1 min-w-0">
          <div className="flex items-center justify-between mb-3">
            <h3 className="text-xl font-bold text-slate-700">
              {showActions ? lead.contactFirstName : lead.contactFullName}
            </h3>
            <div className="flex items-center gap-2">
              <span className="text-lg font-bold text-slate-600">
                ${lead.price.toFixed(2)} Lead Invitation
              </span>
              {!showActions && (
                <span className="px-3 py-1 bg-teal-500 text-white text-xs font-semibold rounded-full">
                  Accepted
                </span>
              )}
            </div>
          </div>

          <div className="flex items-center gap-3 text-sm text-gray-500 mb-3">
            <div className="flex items-center gap-1.5 bg-slate-50 px-3 py-1.5 rounded-lg">
              <Calendar className="w-4 h-4 text-slate-600" />
              <span className="font-medium text-slate-700">{formatDate(lead.createdAt)}</span>
            </div>
            <div className="flex items-center gap-1.5 bg-gray-50 px-3 py-1.5 rounded-lg">
              <Briefcase className="w-4 h-4 text-gray-600" />
              <span className="font-medium text-gray-700">ID: {lead.id.substring(0, 8)}</span>
            </div>
          </div>

          <div className="flex items-center gap-3 text-sm mb-4">
            <div className="flex items-center gap-1.5 bg-blue-50 px-3 py-1.5 rounded-lg">
              <MapPin className="w-4 h-4 text-blue-600" />
              <span className="font-medium text-blue-700">{lead.suburb}</span>
            </div>
            <div className="flex items-center gap-1.5 bg-slate-50 px-3 py-1.5 rounded-lg">
              <Tag className="w-4 h-4 text-slate-600" />
              <span className="font-medium text-slate-700">{lead.category}</span>
            </div>
          </div>

          {!showActions && (
            <div className="flex items-center gap-3 text-sm mb-4">
              <div className="flex items-center gap-1.5 bg-teal-50 px-3 py-1.5 rounded-lg">
                <Phone className="w-4 h-4 text-teal-600" />
                <span className="font-medium text-teal-700">{lead.contactPhoneNumber}</span>
              </div>
              <div className="flex items-center gap-1.5 bg-cyan-50 px-3 py-1.5 rounded-lg">
                <Mail className="w-4 h-4 text-cyan-600" />
                <span className="font-medium text-cyan-700">{lead.contactEmail}</span>
              </div>
            </div>
          )}

          <div className="bg-slate-50 border-l-4 border-slate-400 p-4 rounded-r-lg mb-4">
            <p className="text-gray-700 leading-relaxed">{lead.description}</p>
          </div>

          {showActions && (
            <div className="flex gap-3">
              <button
                onClick={() => onAccept(lead.id)}
                className="flex items-center gap-2 px-6 py-3 bg-teal-500 text-white rounded-lg hover:bg-teal-600 transition-all duration-300 font-semibold shadow-md hover:shadow-lg"
              >
                <CheckCircle2 className="w-5 h-5" />
                Accept
              </button>
              <button
                onClick={() => onDecline(lead.id)}
                className="flex items-center gap-2 px-6 py-3 bg-gray-300 text-gray-700 rounded-lg hover:bg-gray-400 transition-all duration-300 font-semibold shadow-md hover:shadow-lg"
              >
                <XCircle className="w-5 h-5" />
                Decline
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default LeadCard;
