using E_Invoice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Invoice.Infrastructure.Data
{
    public class EInvoiceDbContext : DbContext
    {
        public EInvoiceDbContext(DbContextOptions<EInvoiceDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EInvoiceDbContext).Assembly);
        }

        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Issuer> Issuers { get; set; }
        public DbSet<IssuerAddress> IssuerAddresses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<ReceiverAddress> ReceiverAddresses { get; set; }
        public DbSet<Signature> Signatures { get; set; }
        public DbSet<TaxableItem> TaxableItems { get; set; }
        public DbSet<TaxTotal> TaxTotals { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<DocumentSubmission> DocumentSubmissions { get; set; }
        public DbSet<AcceptedDocument> AcceptedDocuments { get; set; }
    }
}
