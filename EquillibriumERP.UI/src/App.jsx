import { BrowserRouter, Routes, Route } from "react-router-dom";

import Index from "./pages/Index";

import Login from "./modules/authentication/pages/Login";
import CreateERP from "./modules/onboarding/pages/CreateERP";

import ProtectedRoute from "./modules/authentication/components/ProtectedRoute";

import WorkspaceShell from "./modules/workspace/components/WorkspaceShell";

import Dashboard from "./modules/dashboard/pages/Dashboard";

import Sales from "./modules/sales/pages/Sales";
import CustomerPage from "./modules/sales/pages/CustomerPage";
import EstimatePage from "./modules/sales/pages/EstimatePage";
import Invoices from "./modules/sales/pages/Invoices";
import SalesOrders from "./modules/sales/pages/SalesOrders";
import CustomerCategoriesPage from "./modules/sales/pages/CustomerCategoriesPage";

import Inventory from "./modules/inventory/pages/Inventory";
import ProductPage from "./modules/inventory/pages/ProductPage";
import ProductCategoriesPage from "./modules/inventory/pages/ProductCategoriesPage";

import Manufacturing from "./modules/manufacturing/pages/Manufacturing";
import BomPage from "./modules/manufacturing/pages/BomPage";
import WorkOrdersPage from "./modules/manufacturing/pages/WorkOrdersPage";

import Purchasing from "./modules/purchasing/pages/Purchasing";
import SupplierPage from "./modules/purchasing/pages/SupplierPage";

import Quality from "./modules/quality/pages/Quality";

import Lims from "./modules/lims/pages/Lims";
//import SalesOrders from "./modules/sales/pages/SalesOrders";


function App() {

    return (

        <BrowserRouter>

            <Routes>

                <Route
                    path="/"
                    element={<Index />}
                />

                <Route
                    path="/login"
                    element={<Login />}
                />

                <Route
                    path="/create-erp"
                    element={<CreateERP />}
                />

                <Route
                    path="/workspace"
                    element={
                        <ProtectedRoute>
                            <WorkspaceShell />
                        </ProtectedRoute>
                    }
                >

                    <Route
                        index
                        element={<Dashboard />}
                    />

                    <Route path="sales">

                        <Route
                            index
                            element={<Sales />}
                        />

                        <Route
                            path="customers"
                            element={<CustomerPage />}
                        />

                        <Route
                            path="customer-categories"
                            element={<CustomerCategoriesPage />}
                        />

                        <Route
                            path="quotations"
                            element={<EstimatePage />}
                        />

                        <Route
                            path="invoices"
                            element={<Invoices />}
                        />

                        <Route
                            path="salesorders"
                            element={<SalesOrders />}
                        />

                    </Route>

                    <Route path="inventory">

                        <Route
                            index
                            element={<Inventory />}
                        />

                        <Route
                            path="product-categories"
                            element={<ProductCategoriesPage />}
                        />

                        <Route
                            path="products"
                            element={<ProductPage />}
                        />
                        

                    </Route>

                    <Route path="manufacturing">

                        <Route
                            index
                            element={<Manufacturing />}
                        />

                        <Route
                            path="boms"
                            element={<BomPage />}
                        />

                        <Route
                            path="work-orders"
                            element={<WorkOrdersPage />}
                        />

                    </Route>

                    <Route path="purchasing">

                        <Route
                            index
                            element={<Purchasing />}
                        />
                        <Route
                            path="suppliers"
                            element={<SupplierPage />}
                        />


                    </Route>

                    <Route path="quality">

                        <Route
                            index
                            element={<Quality />}
                        />

                    </Route>

                    <Route path="lims">

                        <Route
                            index
                            element={<Lims />}
                        />

                    </Route>

                </Route>

            </Routes>

        </BrowserRouter>

    );

}

export default App;