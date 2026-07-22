import "./ERPTabs.css";

export default function ERPTabs({

    tabs = [],

    activeTab,

    onChange

}) {

    return (

        <div className="erp-tabs">

            {

                tabs.map(tab => (

                    <button

                        key={tab.key}

                        type="button"

                        className={
                            activeTab === tab.key
                                ? "erp-tab active"
                                : "erp-tab"
                        }

                        onClick={() => onChange(tab.key)}

                    >

                        {tab.label}

                    </button>

                ))

            }

        </div>

    );

}