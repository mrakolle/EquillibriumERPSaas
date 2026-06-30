using EquillibriumERP.Products.Contracts.Seeders;

namespace EquillibriumERP.Products.Infrastructure.Seeders;

public static class RawMaterialCatalog
{
    public static IReadOnlyCollection<RawMaterialSeed> Materials =>
    [
        new("ETH96", "Ethanol 96%", "64-17-5"),
        new("IPA", "Isopropyl Alcohol", "67-63-0"),
        new("H2O2", "Hydrogen Peroxide", "7722-84-1"),
        new("GLYCERIN", "Glycerine", "56-81-5"),
        new("CARB940", "Carbomer 940", "9003-01-4"),
        new("TEA", "Triethanolamine", "102-71-6"),
        new("BENZALK", "Benzalkonium Chloride", "8001-54-5"),
        new("CHG", "Chlorhexidine Gluconate", "18472-51-0"),
        new("PHENOXY", "Phenoxyethanol", "122-99-6"),

        new("SLES70", "SLES 70%", "68585-34-2"),
        new("SLES25", "SLES 25%", "68585-34-2"),
        new("LABSA", "Linear Alkyl Benzene Sulphonic Acid", "27176-87-0"),
        new("CAPB", "Cocamidopropyl Betaine", "61789-40-0"),
        new("CDEA", "Cocamide DEA", "68603-42-9"),
        new("AOS", "Alpha Olefin Sulfonate", "68439-57-6"),
        new("NP9", "Nonyl Phenol Ethoxylate 9", "9016-45-9"),
        new("NP10", "Nonyl Phenol Ethoxylate 10", "9016-45-9"),

        new("STPP", "Sodium Tripolyphosphate", "7758-29-4"),
        new("SULPHATE", "Sodium Sulphate", "7757-82-6"),
        new("SODAASHL", "Soda Ash Light", "497-19-8"),
        new("SODAASHD", "Soda Ash Dense", "497-19-8"),
        new("CAUSTICF", "Caustic Soda Flakes", "1310-73-2"),
        new("CAUSTICL", "Caustic Soda Lye", "1310-73-2"),
        new("SODMETA", "Sodium Metasilicate", "6834-92-0"),
        new("SXS", "Sodium Xylene Sulfonate", "1300-72-7"),

        new("EDTA", "EDTA", "6381-92-6"),
        new("CITRIC", "Citric Acid", "77-92-9"),
        new("SODBEN", "Sodium Benzoate", "532-32-1"),
        new("POTSORB", "Potassium Sorbate", "24634-61-5"),

        new("MEA", "Monoethanolamine", "141-43-5"),
        new("DEA", "Diethanolamine", "111-42-2"),
        new("TEA99", "Triethanolamine 99%", "102-71-6"),

        new("BUTYL", "Butyl Glycol", "111-76-2"),
        new("DPM", "Dipropylene Glycol Methyl Ether", "34590-94-8"),
        new("LIMONENE", "D-Limonene", "5989-27-5"),

        new("HCL", "Hydrochloric Acid", "7647-01-0"),
        new("H2SO4", "Sulphuric Acid", "7664-93-9"),
        new("HNO3", "Nitric Acid", "7697-37-2"),
        new("H3PO4", "Phosphoric Acid", "7664-38-2"),
        new("ACETIC", "Acetic Acid", "64-19-7"),

        new("METHANOL", "Methanol", "67-56-1"),
        new("ETHANOL", "Ethanol", "64-17-5"),
        new("ACETONE", "Acetone", "67-64-1"),
        new("MEK", "Methyl Ethyl Ketone", "78-93-3"),

        new("CMC", "Carboxymethyl Cellulose", "9004-32-4"),
        new("XANTHAN", "Xanthan Gum", "11138-66-2"),
        new("GUAR", "Guar Gum", "9000-30-0"),
        new("HEC", "Hydroxyethyl Cellulose", "9004-62-0"),

        new("AMYLASE", "Amylase Enzyme", "9000-90-2"),
        new("PROTEASE", "Protease Enzyme", "9014-01-1"),
        new("LIPASE", "Lipase Enzyme", "9001-62-1"),
        new("CELLULASE", "Cellulase Enzyme", "9012-54-8"),

        new("BKC50", "Benzalkonium Chloride 50%", "8001-54-5"),
        new("DDAC", "Didecyl Dimethyl Ammonium Chloride", "7173-51-5"),
        new("PCMX", "Chloroxylenol", "88-04-0"),
        new("GLUT", "Glutaraldehyde", "111-30-8"),

        new("MEG", "Mono Ethylene Glycol", "107-21-1"),
        new("DEG", "Di Ethylene Glycol", "111-46-6"),

        new("TIO2", "Titanium Dioxide", "13463-67-7"),
        new("FORMALIN", "Formalin", "50-00-0"),
        new("FORMALDEHYDE", "Formaldehyde", "50-00-0"),
        new("BORAX", "Borax", "1303-96-4"),
        new("UREA", "Urea", "57-13-6"),
        new("AMMONIA", "Ammonia Solution", "1336-21-6"),

        new("SOAPNOOD", "Soap Noodles", "61789-31-9"),
        new("OPTICAL", "Optical Brightener", "27344-41-8"),

        new("EM1", "Effective Microorganisms", "N/A"),
        new("MOLASSES", "Molasses", "N/A"),
        new("YUCCA", "Yucca Extract", "223749-05-1")
    ];
}