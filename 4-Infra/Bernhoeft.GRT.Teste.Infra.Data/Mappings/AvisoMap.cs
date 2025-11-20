using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.GRT.Teste.Infra.Data.Mappings
{
    public partial class AvisoMap : IEntityTypeConfiguration<AvisoEntity>
    {
        public void Configure(EntityTypeBuilder<AvisoEntity> builder)
        {
            builder.ToTable("Aviso");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Ativo).HasColumnName(@"ativo").IsRequired();
            builder.Property(x => x.Titulo).HasColumnName(@"titulo").HasColumnType("varchar(50)").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Mensagem).HasColumnName(@"mensagem").HasColumnType("text(2147483647)").IsRequired().IsUnicode(false).HasMaxLength(2147483647);

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<AvisoEntity> builder);
    }

}