using EquillibriumERP.Products.Contracts.Seeders;

namespace EquillibriumERP.Products.Infrastructure.Seeders;

public static class RawMaterialCatalog
{
    public static IReadOnlyCollection<RawMaterialSeed> Materials =>
    [
        // ===================== ALCOHOLS =====================
        new("ETH96", "Ethanol 96%", "Alcohols", "64-17-5"),
        new("IPA", "Isopropyl Alcohol", "Alcohols", "67-63-0"),
        new("METHANOL", "Methanol", "Alcohols", "67-56-1"),

        // ===================== SOLVENTS =====================
        new("ACETONE", "Acetone", "Solvents", "67-64-1"),
        new("MEK", "Methyl Ethyl Ketone", "Solvents", "78-93-3"),
        new("BUTYL", "Butyl Glycol", "Solvents", "111-76-2"),
        new("DPM", "Dipropylene Glycol Methyl Ether", "Solvents", "34590-94-8"),
        new("LIMONENE", "D-Limonene", "Solvents", "5989-27-5"),

        // ===================== ACIDS =====================
        new("HCL", "Hydrochloric Acid", "Acids", "7647-01-0"),
        new("H2SO4", "Sulphuric Acid", "Acids", "7664-93-9"),
        new("HNO3", "Nitric Acid", "Acids", "7697-37-2"),
        new("H3PO4", "Phosphoric Acid", "Acids", "7664-38-2"),
        new("ACETIC", "Acetic Acid", "Acids", "64-19-7"),
        new("CITRIC", "Citric Acid", "Acids", "77-92-9"),

        // ===================== ALKALIS =====================
        new("CAUSTICF", "Caustic Soda Flakes", "Alkalis", "1310-73-2"),
        new("CAUSTICL", "Caustic Soda Lye", "Alkalis", "1310-73-2"),
        new("SODAASHL", "Soda Ash Light", "Alkalis", "497-19-8"),
        new("SODAASHD", "Soda Ash Dense", "Alkalis", "497-19-8"),
        new("SODMETA", "Sodium Metasilicate", "Alkalis", "6834-92-0"),

        // ===================== SURFACTANTS =====================
        new("SLES70", "SLES 70%", "Surfactants", "68585-34-2"),
        new("SLES25", "SLES 25%", "Surfactants", "68585-34-2"),
        new("LABSA", "Linear Alkyl Benzene Sulphonic Acid", "Surfactants", "27176-87-0"),
        new("CAPB", "Cocamidopropyl Betaine", "Surfactants", "61789-40-0"),
        new("AOS", "Alpha Olefin Sulfonate", "Surfactants", "68439-57-6"),

        // ===================== THICKENERS =====================
        new("CMC", "Carboxymethyl Cellulose", "Thickeners", "9004-32-4"),
        new("XANTHAN", "Xanthan Gum", "Thickeners", "11138-66-2"),
        new("CARB940", "Carbomer 940", "Thickeners", "9003-01-4"),

        // ===================== ENZYMES =====================
        new("AMYLASE", "Amylase Enzyme", "Enzymes", "9000-90-2"),
        new("PROTEASE", "Protease Enzyme", "Enzymes", "9014-01-1"),

        // ===================== DISINFECTANTS =====================
        new("BKC50", "Benzalkonium Chloride 50%", "Disinfectants", "8001-54-5"),
        new("CHG", "Chlorhexidine Gluconate", "Disinfectants", "18472-51-0"),

        // ===================== PRESERVATIVES =====================
        new("PHENOXY", "Phenoxyethanol", "Preservatives", "122-99-6"),
        new("SODBEN", "Sodium Benzoate", "Preservatives", "532-32-1"),

        // ===================== NEUTRALISERS =====================
        new("TEA", "Triethanolamine", "Neutralisers", "102-71-6"),
        new("MEA", "Monoethanolamine", "Neutralisers", "141-43-5"),

        // ===================== GLYCOLS =====================
        new("GLYCERIN", "Glycerine", "Glycols", "56-81-5"),
        new("MEG", "Mono Ethylene Glycol", "Glycols", "107-21-1"),

        // ===================== OTHER =====================
        new("TIO2", "Titanium Dioxide", "Colourants", "13463-67-7"),
        new("OPTICAL", "Optical Brightener", "Additives", "27344-41-8"),

        new("BORAX", "Borax", "Builders", "1303-96-4"),
        new("UREA", "Urea", "Additives", "57-13-6"),
        new("AMMONIA", "Ammonia Solution", "Additives", "1336-21-6"),

        new("SOAPNOOD", "Soap Noodles", "Soap Base", "61789-31-9"),

        new("EM1", "Effective Microorganisms", "Bio", "N/A"),
        new("MOLASSES", "Molasses", "Bio", "N/A"),

        new("H2O", "Distilled Water", "Utilities", "7732-18-5")
    ];
}