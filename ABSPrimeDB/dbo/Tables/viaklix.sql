CREATE TABLE [dbo].[viaklix] (
    [transactionid]       NVARCHAR (255) NOT NULL,
    [transactiontype]     NVARCHAR (255) NULL,
    [transactiondatetime] SMALLDATETIME  NULL,
    [approvalcode]        NVARCHAR (53)  NULL,
    [ssl_invoice_number]  NVARCHAR (255) NULL,
    [ssl_amount]          NVARCHAR (53)  NULL,
    [sales_tax]           NVARCHAR (255) NULL,
    [cvv2_indicator]      NVARCHAR (255) NULL,
    [cvv2response]        NVARCHAR (255) NULL,
    [customer_code]       NVARCHAR (53)  NULL,
    [card_number]         NVARCHAR (255) NULL,
    [exp_date]            NVARCHAR (255) NULL,
    [auth_response_code]  NVARCHAR (255) NULL,
    [auth_message]        NVARCHAR (255) NULL,
    [auth_source]         NVARCHAR (53)  NULL,
    [auth_avs_response]   NVARCHAR (255) NULL,
    [status]              NVARCHAR (255) NULL,
    [ssl_address2]        NVARCHAR (255) NULL,
    [ssl_amount1]         NVARCHAR (53)  NULL,
    [ssl_avs_address]     NVARCHAR (255) NULL,
    [ssl_avs_zip]         NVARCHAR (53)  NULL,
    [ssl_card_number]     NVARCHAR (255) NULL,
    [ssl_city]            NVARCHAR (255) NULL,
    [ssl_company]         NVARCHAR (255) NULL,
    [ssl_country]         NVARCHAR (255) NULL,
    [ssl_customer_code]   NVARCHAR (53)  NULL,
    [ssl_email]           NVARCHAR (255) NULL,
    [ssl_exp_date]        NVARCHAR (255) NULL,
    [ssl_first_name]      NVARCHAR (255) NULL,
    [ssl_invoice_number1] NVARCHAR (255) NULL,
    [ssl_last_name]       NVARCHAR (255) NULL,
    [ssl_phone]           NVARCHAR (53)  NULL,
    [ssl_state]           NVARCHAR (255) NULL,
    [ssl_salestax]        NVARCHAR (255) NULL,
    CONSTRAINT [PK_viaklix] PRIMARY KEY NONCLUSTERED ([transactionid] ASC) WITH (FILLFACTOR = 90)
);


GO
CREATE CLUSTERED INDEX [IX_viaklix]
    ON [dbo].[viaklix]([status] ASC) WITH (FILLFACTOR = 90);

