import React from 'react';
import { UserPlus, CheckCircle } from 'lucide-react';

const TabNav = ({ activeTab, setActiveTab }) => {
  const tabs = [
    { id: 'invited', label: 'Invited', icon: UserPlus, count: 0 },
    { id: 'accepted', label: 'Accepted', icon: CheckCircle, count: 0 },
  ];

  return (
    <div className="relative">
      <nav className="flex gap-2 p-2 bg-slate-50 rounded-t-xl">
        {tabs.map((tab) => {
          const Icon = tab.icon;
          const isActive = activeTab === tab.id;
          return (
            <button
              key={tab.id}
              onClick={() => setActiveTab(tab.id)}
              className={`
                flex-1 flex items-center justify-center gap-2 py-4 px-6 text-base font-semibold rounded-lg transition-all duration-300
                ${
                  isActive
                    ? 'bg-slate-600 text-white shadow-md'
                    : 'bg-white text-gray-600 hover:bg-gray-50 hover:text-slate-600 hover:shadow-sm'
                }
              `}
            >
              <Icon className="w-5 h-5" />
              <span>{tab.label}</span>
            </button>
          );
        })}
      </nav>
      <div className="absolute bottom-0 left-0 right-0 h-0.5 bg-slate-300"></div>
    </div>
  );
};

export default TabNav;
