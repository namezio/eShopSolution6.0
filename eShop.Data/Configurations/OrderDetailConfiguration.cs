using eShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Data.Configurations
{
    public class OrderDetailConfiguration: IEntityTypeConfiguration<OderDetail>
    {
        public void Configure(EntityTypeBuilder<OderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => new { x.OderId, x.ProductId });
            builder.HasOne(x => x.Order).WithMany(x => x.OderDetails).HasForeignKey(x => x.OderId);
            builder.HasOne(x => x.Product).WithMany(x => x.OderDetails).HasForeignKey(x => x.ProductId);
        }
    }
}