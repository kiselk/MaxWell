using System;
using MaxWell.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;


namespace MaxWell.Server.Migrations
{
    [DbContext(typeof(MyDatabaseContext))]
    partial class MyDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("DotNetCoreSqlDb.Models.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("TodoItem");
                });
            modelBuilder.Entity("MaxWell.Models.Todo", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

               

                b.Property<byte[]>("DownloadedImageBlob");

                b.HasKey("Id");

                b.ToTable("RemoteImage");
            });
        }
    }
}
