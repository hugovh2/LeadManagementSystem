import React, { useEffect } from 'react';
import { CheckCircle, XCircle, AlertCircle, X } from 'lucide-react';

const Toast = ({ message, type = 'success', onClose, duration = 4000 }) => {
  useEffect(() => {
    const timer = setTimeout(() => {
      onClose();
    }, duration);

    return () => clearTimeout(timer);
  }, [duration, onClose]);

  const icons = {
    success: <CheckCircle className="w-6 h-6 text-green-600" />,
    error: <XCircle className="w-6 h-6 text-red-600" />,
    warning: <AlertCircle className="w-6 h-6 text-yellow-600" />
  };

  const backgrounds = {
    success: 'bg-green-50 border-green-500',
    error: 'bg-red-50 border-red-500',
    warning: 'bg-yellow-50 border-yellow-500'
  };

  return (
    <div className={`fixed top-4 right-4 z-50 flex items-start gap-3 ${backgrounds[type]} border-l-4 rounded-lg shadow-lg p-4 min-w-[320px] max-w-md animate-slideIn`}>
      <div className="flex-shrink-0">
        {icons[type]}
      </div>
      <div className="flex-1">
        <p className="text-sm font-medium text-gray-800">{message}</p>
      </div>
      <button
        onClick={onClose}
        className="flex-shrink-0 text-gray-400 hover:text-gray-600 transition-colors"
      >
        <X className="w-5 h-5" />
      </button>
    </div>
  );
};

export default Toast;
