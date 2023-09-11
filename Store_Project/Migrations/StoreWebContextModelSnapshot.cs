﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Models;

#nullable disable

namespace Store_Asp.net_core.Migrations
{
    [DbContext(typeof(StoreWebContext))]
    partial class StoreWebContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("Store.Models.CategoryModel", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("IdCategory");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("Store.Models.ItemOrderModel", b =>
                {
                    b.Property<int>("IdOrder")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdProduct")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("REAL");

                    b.HasKey("IdOrder", "IdProduct");

                    b.HasIndex("IdProduct");

                    b.ToTable("ItemOrder");
                });

            modelBuilder.Entity("Store.Models.OrderModel", b =>
                {
                    b.Property<int>("IdOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeliveryData")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("OrderData")
                        .HasColumnType("TEXT");

                    b.Property<double?>("TotalValue")
                        .HasColumnType("REAL");

                    b.HasKey("IdOrder");

                    b.HasIndex("IdCustomer");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Store.Models.ProductModel", b =>
                {
                    b.Property<int>("IdProduct")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdCategory")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("Stock")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.HasKey("IdProduct");

                    b.HasIndex("IdCategory");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Store.Models.UserModel", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RegistrationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("datetime('now', 'localtime', 'start of day')");

                    b.HasKey("IdUser");

                    b.ToTable("User");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Store.Models.CustomerModel", b =>
                {
                    b.HasBaseType("Store.Models.UserModel");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("char(14)");

                    b.Property<DateTime>("DateBirth")
                        .HasColumnType("TEXT");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Store.Models.ItemOrderModel", b =>
                {
                    b.HasOne("Store.Models.OrderModel", "Order")
                        .WithMany("OrderedItems")
                        .HasForeignKey("IdOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Store.Models.ProductModel", "Product")
                        .WithMany()
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Store.Models.OrderModel", b =>
                {
                    b.HasOne("Store.Models.CustomerModel", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Store.Models.AddressModel", "DeliveryAddress", b1 =>
                        {
                            b1.Property<int>("OrderModelIdOrder")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Complement")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Neighborhood")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Reference")
                                .HasColumnType("TEXT");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("char(2)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("char(9)");

                            b1.HasKey("OrderModelIdOrder");

                            b1.ToTable("Order", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("OrderModelIdOrder");
                        });

                    b.Navigation("Customer");

                    b.Navigation("DeliveryAddress");
                });

            modelBuilder.Entity("Store.Models.ProductModel", b =>
                {
                    b.HasOne("Store.Models.CategoryModel", "Category")
                        .WithMany("Products")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Store.Models.CustomerModel", b =>
                {
                    b.HasOne("Store.Models.UserModel", null)
                        .WithOne()
                        .HasForeignKey("Store.Models.CustomerModel", "IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Store.Models.AddressModel", "Addresses", b1 =>
                        {
                            b1.Property<int>("IdAddress")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER");

                            b1.Property<int>("IdUser")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Complement")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Neighborhood")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Reference")
                                .HasColumnType("TEXT");

                            b1.Property<bool>("Selected")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("char(2)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("char(9)");

                            b1.HasKey("IdAddress", "IdUser");

                            b1.HasIndex("IdUser");

                            b1.ToTable("Address");

                            b1.WithOwner()
                                .HasForeignKey("IdUser");
                        });

                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Store.Models.CategoryModel", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Store.Models.OrderModel", b =>
                {
                    b.Navigation("OrderedItems");
                });

            modelBuilder.Entity("Store.Models.CustomerModel", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
