CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "CustomerCategories" (
    "Id" uuid NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Description" text,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_CustomerCategories" PRIMARY KEY ("Id")
);

CREATE TABLE "Customers" (
    "Id" uuid NOT NULL,
    "CustomerCode" character varying(50) NOT NULL,
    "Name" character varying(200) NOT NULL,
    "Email" character varying(150) NOT NULL,
    "PhoneNumber" character varying(50) NOT NULL,
    "IsActive" boolean NOT NULL,
    "CustomerCategoryId" uuid,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Customers" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Customers_CustomerCategories_CustomerCategoryId" FOREIGN KEY ("CustomerCategoryId") REFERENCES "CustomerCategories" ("Id") ON DELETE SET NULL
);

CREATE TABLE "CustomerAddresses" (
    "Id" uuid NOT NULL,
    "CustomerId" uuid NOT NULL,
    "AddressLine1" character varying(250) NOT NULL,
    "AddressLine2" text,
    "City" character varying(100) NOT NULL,
    "Province" character varying(100) NOT NULL,
    "PostalCode" character varying(20) NOT NULL,
    "Country" character varying(100) NOT NULL,
    "IsPrimary" boolean NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_CustomerAddresses" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CustomerAddresses_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CustomerContacts" (
    "Id" uuid NOT NULL,
    "CustomerId" uuid NOT NULL,
    "FirstName" character varying(100) NOT NULL,
    "LastName" character varying(100) NOT NULL,
    "Email" character varying(150) NOT NULL,
    "PhoneNumber" character varying(50) NOT NULL,
    "Position" text,
    "IsPrimary" boolean NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_CustomerContacts" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CustomerContacts_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CustomerCreditProfiles" (
    "Id" uuid NOT NULL,
    "CustomerId" uuid NOT NULL,
    "CreditLimit" numeric(18,2) NOT NULL,
    "CurrentBalance" numeric(18,2) NOT NULL,
    "PaymentTermsInDays" integer NOT NULL,
    "IsOnHold" boolean NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_CustomerCreditProfiles" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CustomerCreditProfiles_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_CustomerAddresses_CustomerId" ON "CustomerAddresses" ("CustomerId");

CREATE INDEX "IX_CustomerContacts_CustomerId" ON "CustomerContacts" ("CustomerId");

CREATE UNIQUE INDEX "IX_CustomerCreditProfiles_CustomerId" ON "CustomerCreditProfiles" ("CustomerId");

CREATE INDEX "IX_Customers_CustomerCategoryId" ON "Customers" ("CustomerCategoryId");

CREATE UNIQUE INDEX "IX_Customers_CustomerCode" ON "Customers" ("CustomerCode");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260514192959_TenantInitial', '10.0.7');

COMMIT;

START TRANSACTION;
CREATE TABLE "Estimates" (
    "Id" uuid NOT NULL,
    "EstimateNumber" character varying(50) NOT NULL,
    "CustomerId" uuid NOT NULL,
    "EstimateDateUtc" timestamp with time zone NOT NULL,
    "ExpiryDateUtc" timestamp with time zone NOT NULL,
    "Status" integer NOT NULL,
    "Subtotal" numeric(18,2) NOT NULL,
    "TaxAmount" numeric(18,2) NOT NULL,
    "TotalAmount" numeric(18,2) NOT NULL,
    "Notes" character varying(2000) NOT NULL,
    CONSTRAINT "PK_Estimates" PRIMARY KEY ("Id")
);

CREATE TABLE "Invoices" (
    "Id" uuid NOT NULL,
    "InvoiceNumber" character varying(50) NOT NULL,
    "CustomerId" uuid NOT NULL,
    "EstimateId" uuid,
    "InvoiceDateUtc" timestamp with time zone NOT NULL,
    "DueDateUtc" timestamp with time zone NOT NULL,
    "Status" integer NOT NULL,
    "Subtotal" numeric(18,2) NOT NULL,
    "TaxAmount" numeric(18,2) NOT NULL,
    "TotalAmount" numeric(18,2) NOT NULL,
    "PaidAmount" numeric(18,2) NOT NULL,
    "Notes" character varying(2000) NOT NULL,
    CONSTRAINT "PK_Invoices" PRIMARY KEY ("Id")
);

CREATE TABLE "EstimateItems" (
    "Id" uuid NOT NULL,
    "EstimateId" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "Description" character varying(500) NOT NULL,
    "Quantity" numeric(18,2) NOT NULL,
    "UnitPrice" numeric(18,2) NOT NULL,
    "TaxRate" numeric(5,2) NOT NULL,
    "LineSubtotal" numeric(18,2) NOT NULL,
    "TaxAmount" numeric(18,2) NOT NULL,
    "LineTotal" numeric(18,2) NOT NULL,
    CONSTRAINT "PK_EstimateItems" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_EstimateItems_Estimates_EstimateId" FOREIGN KEY ("EstimateId") REFERENCES "Estimates" ("Id") ON DELETE CASCADE
);

CREATE TABLE "InvoiceItems" (
    "Id" uuid NOT NULL,
    "InvoiceId" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "Description" character varying(500) NOT NULL,
    "Quantity" numeric(18,2) NOT NULL,
    "UnitPrice" numeric(18,2) NOT NULL,
    "TaxRate" numeric(5,2) NOT NULL,
    "LineSubtotal" numeric(18,2) NOT NULL,
    "TaxAmount" numeric(18,2) NOT NULL,
    "LineTotal" numeric(18,2) NOT NULL,
    CONSTRAINT "PK_InvoiceItems" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_InvoiceItems_Invoices_InvoiceId" FOREIGN KEY ("InvoiceId") REFERENCES "Invoices" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_EstimateItems_EstimateId" ON "EstimateItems" ("EstimateId");

CREATE UNIQUE INDEX "IX_Estimates_EstimateNumber" ON "Estimates" ("EstimateNumber");

CREATE INDEX "IX_InvoiceItems_InvoiceId" ON "InvoiceItems" ("InvoiceId");

CREATE UNIQUE INDEX "IX_Invoices_InvoiceNumber" ON "Invoices" ("InvoiceNumber");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260514210539_AddSalesCommercialDocuments', '10.0.7');

COMMIT;

START TRANSACTION;
CREATE TABLE "ProductCategories" (
    "Id" uuid NOT NULL,
    "Name" character varying(150) NOT NULL,
    "Description" character varying(1000) NOT NULL,
    CONSTRAINT "PK_ProductCategories" PRIMARY KEY ("Id")
);

CREATE TABLE "Products" (
    "Id" uuid NOT NULL,
    "ProductCode" character varying(50) NOT NULL,
    "Name" character varying(200) NOT NULL,
    "Description" character varying(2000) NOT NULL,
    "ProductCategoryId" uuid,
    "ProductType" integer NOT NULL,
    "SellingPrice" numeric(18,2) NOT NULL,
    "CostPrice" numeric(18,2) NOT NULL,
    "IsActive" boolean NOT NULL,
    CONSTRAINT "PK_Products" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Products_ProductCategories_ProductCategoryId" FOREIGN KEY ("ProductCategoryId") REFERENCES "ProductCategories" ("Id") ON DELETE SET NULL
);

CREATE INDEX "IX_Products_ProductCategoryId" ON "Products" ("ProductCategoryId");

CREATE UNIQUE INDEX "IX_Products_ProductCode" ON "Products" ("ProductCode");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260514211925_AddCommercialCore', '10.0.7');

COMMIT;

START TRANSACTION;
ALTER TABLE "Products" SET SCHEMA public;

ALTER TABLE "ProductCategories" SET SCHEMA public;

ALTER TABLE "Invoices" SET SCHEMA public;

ALTER TABLE "InvoiceItems" SET SCHEMA public;

ALTER TABLE "Estimates" SET SCHEMA public;

ALTER TABLE "EstimateItems" SET SCHEMA public;

ALTER TABLE "Customers" SET SCHEMA public;

ALTER TABLE "CustomerCreditProfiles" SET SCHEMA public;

ALTER TABLE "CustomerContacts" SET SCHEMA public;

ALTER TABLE "CustomerCategories" SET SCHEMA public;

ALTER TABLE "CustomerAddresses" SET SCHEMA public;

CREATE TABLE public."EstimateSequences" (
    "Id" uuid NOT NULL,
    "Year" integer NOT NULL,
    "LastNumber" bigint NOT NULL,
    CONSTRAINT "PK_EstimateSequences" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX "IX_EstimateSequences_Year" ON public."EstimateSequences" ("Year");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260521083400_AddEstimateSequence', '10.0.7');

COMMIT;

