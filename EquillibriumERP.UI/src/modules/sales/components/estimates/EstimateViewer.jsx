import { useEffect, useState } from "react";

import {
    getEstimate,
    updateEstimate
} from "../../services/estimateService";

import {
    ERPActionBar,
    ERPButton,
    ERPCard,
    ERPDetailsLayout,
    ERPImageCard,
    ERPDocumentViewer,
    ERPDocumentModal,
    ERPCompanyHeader,
    ERPDocumentCustomerBlock,
    ERPDocumentTable,
    ERPDocumentFooterLayout,
    ERPDocumentPaymentTerms,
    ERPDocumentNotes,
    ERPDocumentBankingDetails,
    ERPDocumentSignature
} from "../../../../components/erp";

import EstimateDocumentTable
    from "./documents/EstimateDocumentTable";

import EstimateInformationForm
    from "./EstimateInformationForm";

import EstimateItemsGrid
    from "./EstimateItemsGrid";

import EstimateTotals
    from "./EstimateTotals";


export default function EstimateViewer({

    estimateId,

    onCancel

}) {


    const [estimate, setEstimate] = useState(null);

    const [items, setItems] = useState([]);

    const [loading, setLoading] = useState(true);

    const [error, setError] = useState(null);

    const [isEditing, setIsEditing] = useState(false);

    const [isSaving, setIsSaving] = useState(false);

    const [showPreview, setShowPreview] = useState(false);


    useEffect(() => {

        loadEstimate();

    }, [estimateId]);



    async function loadEstimate() {

        setLoading(true);

        setError(null);

        try {

            const data =
                await getEstimate(estimateId);

            setEstimate(data);

            setItems(data.items ?? []);

        }
        catch (error) {

            console.error(error);

            setError("Unable to load quotation.");

        }
        finally {

            setLoading(false);

        }

    }



    async function saveEstimate() {

        if (isSaving) {

            return;

        }

        setIsSaving(true);

        try {

            await updateEstimate(

                estimate.id,

                estimate,

                items

            );

            alert("Quotation updated successfully.");

            onCancel();

        }
        catch (error) {

            console.error(error);

            alert("Failed to update quotation.");

        }
        finally {

            setIsSaving(false);

        }

    }



    if (loading) {

        return <div>Loading quotation...</div>;

    }


    if (error) {

        return <div>{error}</div>;

    }


    if (!estimate) {

        return <div>Quotation not found.</div>;

    }



    return (

        <ERPCard

            title="Quotation"

            subtitle={estimate.quoteNumber}

        >


            <ERPDetailsLayout

                left={

                    <EstimateInformationForm

                        estimate={estimate}

                        onChange={setEstimate}

                        isEditing={isEditing}

                        isNew={false}

                    />

                }

                right={

                    <ERPImageCard

                        title="Customer"

                        caption={estimate.customerName}

                    />

                }

            />



            <EstimateItemsGrid

                items={items}

                onChange={setItems}

                isEditing={isEditing}

            />



            <EstimateTotals

                items={items}

            />



            <ERPActionBar>


                <ERPButton

                    onClick={() =>
                        isEditing
                            ? saveEstimate()
                            : setIsEditing(true)
                    }

                    disabled={isSaving}

                >

                    {
                        isEditing
                            ? "Save"
                            : "Edit"
                    }

                </ERPButton>



                <ERPButton

                    variant="secondary"

                    onClick={() =>
                        setShowPreview(true)
                    }

                >

                    Preview

                </ERPButton>



                <ERPButton

                    variant="secondary"

                    onClick={() => window.print()}

                >

                    Print

                </ERPButton>



                <ERPButton

                    variant="secondary"

                    onClick={() =>
                        alert("PDF generation coming next.")
                    }

                >

                    Download PDF

                </ERPButton>



                {
                    isEditing && (

                        <ERPButton

                            variant="secondary"

                            onClick={() => {

                                setIsEditing(false);

                                loadEstimate();

                            }}

                        >

                            Cancel

                        </ERPButton>

                    )
                }



                <ERPButton

                    variant="secondary"

                    onClick={onCancel}

                >

                    ← Back

                </ERPButton>


            </ERPActionBar>




            {
                showPreview && (

                    <ERPDocumentModal

                        title="Quotation Preview"

                        onClose={() =>
                            setShowPreview(false)
                        }

                        onPrint={() =>
                            window.print()
                        }

                        onDownload={() => {

                            alert(
                                "PDF generation coming next."
                            );

                        }}

                    >


                        <ERPDocumentViewer


                            header={

                                <ERPCompanyHeader

                                    companyName="IQuillibrium ERP"

                                    companyAddress="Your Company Address"

                                    companyPhone="+27 XX XXX XXXX"

                                    companyEmail="info@iquillibrium.com"

                                    companyWebsite="www.iquillibrium.com"

                                    title="QUOTATION"

                                    documentNumber={
                                
                                        estimate.quoteNumber
                                        
                                    
                                    }

                                />

                            }



                            body={

    <>

                                <ERPDocumentCustomerBlock

                                    customerName={estimate.customerName}
                                    customerPhone={estimate.customerPhone}
                                    customerEmail={estimate.customerEmail}
                                    customerVatNumber={estimate.customerVatNumber}

                                    quotationNumber={estimate.quoteNumber}

                                    reference={estimate.reference}
                                    documentDate={estimate.estimateDateUtc}
                                    expiryDate={estimate.expiryDateUtc}

                                />


                                    <ERPDocumentTable>

                                        <EstimateDocumentTable

                                            items={items}

                                        />

                                    </ERPDocumentTable>

                                </>

                            }



                           footer={

    <>

        <ERPDocumentFooterLayout

            left={

                <ERPDocumentBankingDetails

                    bankName="First National Bank"

                    accountName="IQuillibrium ERP"

                    accountNumber="00000000000"

                    accountType="Business Cheque"

                    branchName="Sandton"

                    branchCode="250655"

                    swiftCode="FIRNZAJJ"

                    reference={estimate.quoteNumber}

                />

            }

            right={

                <EstimateTotals

                    items={items}

                />

            }

        />

        <ERPDocumentPaymentTerms

            validity="30 Days"

            paymentTerms="50% Deposit, Balance on Completion"

            delivery="Excluded unless otherwise stated"

            pricesIncludeVat={false}

        />

        <ERPDocumentNotes

            notes={[

                "Lead time: 10–15 working days from receipt of deposit.",

                "Installation is excluded unless specifically quoted.",

                "Warranty covers manufacturing defects only.",

                "Goods remain the property of the seller until paid in full."

            ]}

        />

        <ERPDocumentSignature

            preparedBy="Prepared By"

            approvedBy="Accepted By"

        />

    </>

}

/>


                    </ERPDocumentModal>

                )
            }


        </ERPCard>

    );

}