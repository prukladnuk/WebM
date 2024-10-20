using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class MessangerContext : DbContext
{
    public MessangerContext()
    {
    }

    public MessangerContext(DbContextOptions<MessangerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatFile> ChatFiles { get; set; }

    public virtual DbSet<ForwardMessage> ForwardMessages { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Reaction> Reactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-CS0FRDE;Initial Catalog=WebMessanger;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Icon).HasColumnType("image");
            entity.Property(e => e.IsChannel).HasColumnName("Is_Channel");
            entity.Property(e => e.IsGroup).HasColumnName("Is_Group");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ChatFile>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PK_Medias");

            entity.Property(e => e.ChatIdFk).HasColumnName("ChatId_FK");
            entity.Property(e => e.FileType)
                .HasMaxLength(50)
                .HasColumnName("File_Type");
            entity.Property(e => e.FileUrl)
                .HasMaxLength(450)
                .HasColumnName("File_Url");
            entity.Property(e => e.UploatedAt)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("Uploated_At");
            entity.Property(e => e.UserIdFk)
                .HasMaxLength(450)
                .HasColumnName("UserID_FK");

            entity.HasOne(d => d.ChatIdFkNavigation).WithMany(p => p.ChatFiles)
                .HasForeignKey(d => d.ChatIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatFiles_ChatID");

            entity.HasOne(d => d.UserIdFkNavigation).WithMany(p => p.ChatFiles)
                .HasForeignKey(d => d.UserIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatFiles_UserID");
        });

        modelBuilder.Entity<ForwardMessage>(entity =>
        {
            entity.HasKey(e => e.ForwardId);

            entity.Property(e => e.ForwardAt)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("Forward_At");
            entity.Property(e => e.NewChatIdFk).HasColumnName("NewChatId_FK");
            entity.Property(e => e.OriginalIsDeleted).HasColumnName("OriginalIs_Deleted");
            entity.Property(e => e.OriginalMessageIdFk).HasColumnName("OriginalMessageId_FK");

            entity.HasOne(d => d.NewChatIdFkNavigation).WithMany(p => p.ForwardMessages)
                .HasForeignKey(d => d.NewChatIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ForwardMessages_ChatID");

            entity.HasOne(d => d.OriginalMessageIdFkNavigation).WithMany(p => p.ForwardMessages)
                .HasForeignKey(d => d.OriginalMessageIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ForwardMessages_MessageID");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.Property(e => e.ChatIdFk).HasColumnName("ChatId_FK");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.EditedAt)
                .HasColumnType("datetime")
                .HasColumnName("Edited_At");
            entity.Property(e => e.FileIdFk).HasColumnName("FileID_FK");
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.IsForwarded).HasColumnName("Is_Forwarded");
            entity.Property(e => e.IsRead).HasColumnName("Is_Read");
            entity.Property(e => e.MessageType)
                .HasMaxLength(50)
                .HasColumnName("Message_Type");
            entity.Property(e => e.SentAt)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("Sent_At");
            entity.Property(e => e.UserIdFk)
                .HasMaxLength(450)
                .HasColumnName("UserID_FK");

            entity.HasOne(d => d.ChatIdFkNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_ChatID");

            entity.HasOne(d => d.FileIdFkNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.FileIdFk)
                .HasConstraintName("FK_Messages_FileId");

            entity.HasOne(d => d.UserIdFkNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_UserID");
        });

        modelBuilder.Entity<Reaction>(entity =>
        {
            entity.Property(e => e.ReactionId).HasColumnName("Reaction_Id");
            entity.Property(e => e.MessageIdFk).HasColumnName("MessageId_FK");
            entity.Property(e => e.ReactedAt)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("Reacted_At");
            entity.Property(e => e.ReactionType)
                .HasMaxLength(50)
                .HasColumnName("Reaction_Type");
            entity.Property(e => e.UserIdFk)
                .HasMaxLength(450)
                .HasColumnName("UserID_FK");

            entity.HasOne(d => d.MessageIdFkNavigation).WithMany(p => p.Reactions)
                .HasForeignKey(d => d.MessageIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reactions_MessageID");

            entity.HasOne(d => d.UserIdFkNavigation).WithMany(p => p.Reactions)
                .HasForeignKey(d => d.UserIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reactions_UserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
