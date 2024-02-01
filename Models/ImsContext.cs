using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IMS.Models;

public partial class ImsContext : DbContext
{
    public ImsContext()
    {
    }

    public ImsContext(DbContextOptions<ImsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepartmentMaster> DepartmentMasters { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<Employeemst> Employeemsts { get; set; }

    public virtual DbSet<IssuedMaster> IssuedMasters { get; set; }

    public virtual DbSet<ItemWork> ItemWorks { get; set; }

    public virtual DbSet<LoginMaster> LoginMasters { get; set; }

    public virtual DbSet<ProductDeptMaster> ProductDeptMasters { get; set; }

    public virtual DbSet<ProductMaster> ProductMasters { get; set; }

    public virtual DbSet<SubItemsMaster> SubItemsMasters { get; set; }

    public virtual DbSet<SubProductMaster> SubProductMasters { get; set; }

    public virtual DbSet<TaxMaster> TaxMasters { get; set; }

    public virtual DbSet<ThroghMaster> ThroghMasters { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(u => u.Id);
        modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles").HasKey(r => r.Id);
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles").HasKey(ur => new { ur.UserId, ur.RoleId });
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims").HasKey(uc => uc.Id);
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins").HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens").HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
        modelBuilder.Entity<DepartmentMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DEPARTMENT MASTER");

            entity.ToTable("DEPARTMENT_MASTER");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .HasColumnName("DEPARTMENT NAME");
            entity.Property(e => e.Fdeptcode)
                .HasMaxLength(3)
                .HasColumnName("FDEPTCODE");
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.ToTable("DESIGNATION");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Fdesigcode)
                .HasMaxLength(3)
                .HasColumnName("FDESIGCODE");
            entity.Property(e => e.Fdesigname)
                .HasMaxLength(50)
                .HasColumnName("FDESIGNAME");
            entity.Property(e => e.Fgat).HasColumnName("fgat");
            entity.Property(e => e.Fgradebkp).HasColumnName("fgradebkp");
            entity.Property(e => e.Fgroup)
                .HasMaxLength(50)
                .HasColumnName("FGROUP");
        });

        modelBuilder.Entity<Employeemst>(entity =>
        {
            entity.ToTable("EMPLOYEEMST");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Aicte)
                .HasMaxLength(50)
                .HasColumnName("AICTE");
            entity.Property(e => e.BiometricCode).HasColumnName("BIOMETRIC_CODE");
            entity.Property(e => e.CategoryType)
                .HasMaxLength(50)
                .HasColumnName("CATEGORY_TYPE");
            entity.Property(e => e.CcaGroup)
                .HasMaxLength(50)
                .HasColumnName("CCA_GROUP");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_on");
            entity.Property(e => e.CurrentBasicPay).HasColumnName("Current_Basic_Pay");
            entity.Property(e => e.CurrentIncrementDate)
                .HasColumnType("date")
                .HasColumnName("Current_Increment_Date");
            entity.Property(e => e.DaCategory)
                .HasMaxLength(50)
                .HasColumnName("DA_CATEGORY");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DateOfRetirement)
                .HasColumnType("date")
                .HasColumnName("Date_of_retirement");
            entity.Property(e => e.El)
                .HasMaxLength(3)
                .HasColumnName("el");
            entity.Property(e => e.EmpIsActive).HasColumnName("EMP_IS_ACTIVE");
            entity.Property(e => e.EmployeeCreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Employee_Created_By");
            entity.Property(e => e.EmployeeStatus)
                .HasMaxLength(50)
                .HasColumnName("Employee_STATUS");
            entity.Property(e => e.EmployeeUpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Employee_Updated_By");
            entity.Property(e => e.Faccno)
                .HasMaxLength(10)
                .HasColumnName("FACCNO");
            entity.Property(e => e.Fapptype)
                .HasMaxLength(50)
                .HasColumnName("FAPPTYPE");
            entity.Property(e => e.Fdeptcode)
                .HasMaxLength(10)
                .HasColumnName("FDEPTCODE");
            entity.Property(e => e.Fdoapp)
                .HasColumnType("datetime")
                .HasColumnName("FDOAPP");
            entity.Property(e => e.Fempcode)
                .HasMaxLength(3)
                .HasColumnName("FEMPCODE");
            entity.Property(e => e.Fempdesig)
                .HasMaxLength(10)
                .HasColumnName("FEMPDESIG");
            entity.Property(e => e.Fempname)
                .HasMaxLength(255)
                .HasColumnName("FEMPNAME");
            entity.Property(e => e.HraLoc)
                .HasMaxLength(100)
                .HasColumnName("HRA_LOC");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(100)
                .HasColumnName("IP_Address");
            entity.Property(e => e.LeaveCl).HasColumnName("LEAVE-CL");
            entity.Property(e => e.LeaveEl).HasColumnName("LEAVE-EL");
            entity.Property(e => e.LeaveRh).HasColumnName("LEAVE-RH");
            entity.Property(e => e.MacAddress)
                .HasMaxLength(100)
                .HasColumnName("MAC_ADDRESS");
            entity.Property(e => e.Nps).HasColumnName("NPS");
            entity.Property(e => e.PayScaleSchemeCode).HasColumnName("PAY_SCALE_SCHEME_CODE");
            entity.Property(e => e.Porder).HasColumnName("PORDER");
            entity.Property(e => e.PreIncrementDate)
                .HasColumnType("date")
                .HasColumnName("Pre_Increment_Date");
            entity.Property(e => e.Print)
                .HasMaxLength(255)
                .HasColumnName("print");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Updated_on");
        });

        modelBuilder.Entity<IssuedMaster>(entity =>
        {
            entity.ToTable("Issued_Master");

            entity.Property(e => e.BookNumber)
                .HasMaxLength(50)
                .HasColumnName("Book_Number");
            entity.Property(e => e.DeptType).HasColumnName("Dept_Type");
            entity.Property(e => e.IssuedDate)
                .HasColumnType("date")
                .HasColumnName("Issued_date");
            entity.Property(e => e.IssuedPerson).HasColumnName("Issued_Person");
            entity.Property(e => e.IssuedPersonDesigntion)
                .HasMaxLength(50)
                .HasColumnName("Issued_Person_designtion");
            entity.Property(e => e.PageNumber)
                .HasMaxLength(50)
                .HasColumnName("Page_Number");
            entity.Property(e => e.ProductName).HasColumnName("Product_Name");
            entity.Property(e => e.Rol).HasMaxLength(50);
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .HasColumnName("serial_Number");
        });

        modelBuilder.Entity<ItemWork>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Item_");

            entity.ToTable("Item_Work");

            entity.Property(e => e.Amount).HasMaxLength(50);
            entity.Property(e => e.ItemDesc).HasColumnName("item_desc");
            entity.Property(e => e.NumOfQuantity).HasColumnName("num_of_quantity");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("date")
                .HasColumnName("purchase_date");
            entity.Property(e => e.ThroghId).HasColumnName("throghId");

            entity.HasOne(d => d.Subtype).WithMany(p => p.ItemWorks)
                .HasForeignKey(d => d.Subtypeid)
                .HasConstraintName("FK_Item_Work_SubItemsMaster");

            entity.HasOne(d => d.TaxNavigation).WithMany(p => p.ItemWorks)
                .HasForeignKey(d => d.Tax)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Work_TaxMaster");
        });

        modelBuilder.Entity<LoginMaster>(entity =>
        {
            entity.ToTable("Login_Master");

            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("User_Name");
        });

        modelBuilder.Entity<ProductDeptMaster>(entity =>
        {
            entity.ToTable("Product_Dept_Master");
        });

        modelBuilder.Entity<ProductMaster>(entity =>
        {
            entity.ToTable("ProductMaster");

            entity.Property(e => e.ProductName)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubItemsMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubItems__3214EC07C08E086F");

            entity.ToTable("SubItemsMaster");

            entity.Property(e => e.MainitemId).HasColumnName("mainitemId");

            entity.HasOne(d => d.Mainitem).WithMany(p => p.SubItemsMasters)
                .HasForeignKey(d => d.MainitemId)
                .HasConstraintName("FK__SubItemsM__maini__5070F446");
        });

        modelBuilder.Entity<SubProductMaster>(entity =>
        {
            entity.ToTable("SubProductMaster");

            entity.Property(e => e.SubProdcutDesc)
                .HasMaxLength(500)
                .HasColumnName("sub_prodcut_desc");

            entity.HasOne(d => d.PidNavigation).WithMany(p => p.SubProductMasters)
                .HasForeignKey(d => d.Pid)
                .HasConstraintName("FK_SubProductMaster_ProductMaster");
        });

        modelBuilder.Entity<TaxMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaxMaste__3214EC07A8A99C38");

            entity.ToTable("TaxMaster");

            entity.Property(e => e.PercentageDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("percentage_desc");
        });

        modelBuilder.Entity<ThroghMaster>(entity =>
        {
            entity.ToTable("Throgh_Master");

            entity.Property(e => e.FromDesc)
                .HasMaxLength(50)
                .HasColumnName("From_desc");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.Property(e => e.VendorId).HasColumnName("Vendor_Id");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .HasColumnName("Email_Address");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.VendorAddress).HasColumnName("Vendor_Address");
            entity.Property(e => e.VendorName).HasColumnName("Vendor_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
