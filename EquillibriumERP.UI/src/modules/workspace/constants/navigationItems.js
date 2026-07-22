import { createNavigationItem } from "../models/navigationItemModel";

export const navigationItems = [

    createNavigationItem(
        "dashboard",
        "Dashboard",
        "/workspace"
    ),

    createNavigationItem(
    "sales",
    "Sales",
    null,
    [

        

        createNavigationItem(
            "quotations",
            "Quotations",
            "/workspace/sales/quotations"
        ),

        createNavigationItem(
            "invoices",
            "Invoices",
            "/workspace/sales/invoices"
        ),
        createNavigationItem(
            "customers",
            "Customers",
            "/workspace/sales/customers"
        ),

        createNavigationItem(
            "customerCategories",
            "Customer Categories",
            "/workspace/sales/customer-categories"
        )

    ]
),

    createNavigationItem(
        "inventory",
        "Inventory",
        null,
        [

            createNavigationItem(
                "product-categories",
                "Product Categories",
                "/workspace/inventory/product-categories"
            ),

            createNavigationItem(
                "products",
                "Products",
                "/workspace/inventory/products"
            ),

            createNavigationItem(
                "stock-items",
                "Stock Items",
                "/workspace/inventory/stock-items"
            ),

            createNavigationItem(
                "stock-movements",
                "Stock Movements",
                "/workspace/inventory/stock-movements"
            )

        ]
    ),

    createNavigationItem(
        "manufacturing",
        "Manufacturing",
        null,
        [
            createNavigationItem(
                "boms",
                "BOMs",
                "/workspace/manufacturing/boms"
            ),

            createNavigationItem(
                "work-orders",
                "Work Orders",
                "/workspace/manufacturing/work-orders"
            )
        ]
    ),

    createNavigationItem(
        "purchasing",
        "Purchasing",
        null,
        [
            createNavigationItem(
                "suppliers",
                "Suppliers",
                "/workspace/purchasing/suppliers"
            ),

            createNavigationItem(
                "purchase-orders",
                "Purchase Orders",
                "/workspace/purchasing/purchase-orders"
            )
        ]
    ),

    createNavigationItem(
        "quality",
        "Quality",
        null,
        [
            createNavigationItem(
                "specifications",
                "Specifications",
                "/workspace/quality/specifications"
            ),

            createNavigationItem(
                "inspections",
                "Inspections",
                "/workspace/quality/inspections"
            ),

            createNavigationItem(
                "non-conformances",
                "Non-Conformances",
                "/workspace/quality/non-conformances"
            ),

            createNavigationItem(
                "capa",
                "CAPA",
                "/workspace/quality/capa"
            )
        ]
    ),

    createNavigationItem(
        "lims",
        "LIMS",
        null,
        [
            createNavigationItem(
                "samples",
                "Samples",
                "/workspace/lims/samples"
            ),

            createNavigationItem(
                "test-requests",
                "Test Requests",
                "/workspace/lims/test-requests"
            ),

            createNavigationItem(
                "test-results",
                "Test Results",
                "/workspace/lims/test-results"
            ),

            createNavigationItem(
                "coa",
                "Certificates of Analysis",
                "/workspace/lims/coa"
            )
        ]
    )

];