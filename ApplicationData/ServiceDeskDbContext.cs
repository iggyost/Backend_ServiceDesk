using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend_ServiceDesk.ApplicationData;

public partial class ServiceDeskDbContext : DbContext
{
    public ServiceDeskDbContext()
    {
    }

    public ServiceDeskDbContext(DbContextOptions<ServiceDeskDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminsRequest> AdminsRequests { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatsMessage> ChatsMessages { get; set; }

    public virtual DbSet<ChatsView> ChatsViews { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessagesView> MessagesViews { get; set; }

    public virtual DbSet<Progress> Progresses { get; set; }

    public virtual DbSet<ReportsView> ReportsViews { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestsView> RequestsViews { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=IgorPc\\SQLEXPRESS; Database=ServiceDeskDb; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
        });

        modelBuilder.Entity<AdminsRequest>(entity =>
        {
            entity.HasKey(e => e.AdminRequestId);

            entity.Property(e => e.AdminRequestId).HasColumnName("admin_request_id");
            entity.Property(e => e.AcceptedDate)
                .HasColumnType("date")
                .HasColumnName("accepted_date");
            entity.Property(e => e.AcceptedTime)
                .HasPrecision(0)
                .HasColumnName("accepted_time");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.IsReady).HasColumnName("isReady");
            entity.Property(e => e.LastChangeDate)
                .HasColumnType("date")
                .HasColumnName("last_change_date");
            entity.Property(e => e.LastChangeTime)
                .HasPrecision(0)
                .HasColumnName("last_change_time");
            entity.Property(e => e.RequestId).HasColumnName("request_id");

            entity.HasOne(d => d.Admin).WithMany(p => p.AdminsRequests)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminsRequests_Admins");

            entity.HasOne(d => d.Request).WithMany(p => p.AdminsRequests)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminsRequests_Requests");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Admin).WithMany(p => p.Chats)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chats_Admins");

            entity.HasOne(d => d.User).WithMany(p => p.Chats)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chats_Users");
        });

        modelBuilder.Entity<ChatsMessage>(entity =>
        {
            entity.HasKey(e => e.ChatMessageId);

            entity.Property(e => e.ChatMessageId).HasColumnName("chat_message_id");
            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.MessageId).HasColumnName("message_id");

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatsMessages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatsMessages_Chats");

            entity.HasOne(d => d.Message).WithMany(p => p.ChatsMessages)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatsMessages_Messages");
        });

        modelBuilder.Entity<ChatsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ChatsView");

            entity.Property(e => e.AdminEmail)
                .HasMaxLength(50)
                .HasColumnName("admin_email");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .HasColumnName("user_email");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Attachment).HasColumnName("attachment");
            entity.Property(e => e.Message1)
                .HasMaxLength(500)
                .HasColumnName("message");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Admin).WithMany(p => p.Messages)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK_Messages_Admins");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Messages_Users");
        });

        modelBuilder.Entity<MessagesView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MessagesView");

            entity.Property(e => e.AdminEmail)
                .HasMaxLength(50)
                .HasColumnName("admin_email");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Attachment).HasColumnName("attachment");
            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .HasColumnName("message");
            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .HasColumnName("user_email");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Progress>(entity =>
        {
            entity.ToTable("Progress");

            entity.Property(e => e.ProgressId).HasColumnName("progress_id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.AdminRequestId).HasColumnName("admin_request_id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.AdminRequest).WithMany(p => p.Progresses)
                .HasForeignKey(d => d.AdminRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Progress_AdminsRequests");
        });

        modelBuilder.Entity<ReportsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ReportsView");

            entity.Property(e => e.AcceptedDate)
                .HasColumnType("date")
                .HasColumnName("accepted_date");
            entity.Property(e => e.AcceptedTime)
                .HasPrecision(0)
                .HasColumnName("accepted_time");
            entity.Property(e => e.AdminEmail)
                .HasMaxLength(50)
                .HasColumnName("admin_email");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Category)
                .HasMaxLength(80)
                .HasColumnName("category");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.IsReady).HasColumnName("isReady");
            entity.Property(e => e.LastChangeDate)
                .HasColumnType("date")
                .HasColumnName("last_change_date");
            entity.Property(e => e.LastChangeTime)
                .HasPrecision(0)
                .HasColumnName("last_change_time");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .HasColumnName("user_email");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Time)
                .HasPrecision(0)
                .HasColumnName("time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Requests)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Categories");

            entity.HasOne(d => d.Status).WithMany(p => p.Requests)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Statuses");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Users");
        });

        modelBuilder.Entity<RequestsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RequestsView");

            entity.Property(e => e.Category)
                .HasMaxLength(80)
                .HasColumnName("category");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Time)
                .HasPrecision(0)
                .HasColumnName("time");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .HasColumnName("user_email");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
