using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyBlog.Models
{
    public partial class MyBlogDbContext : DbContext
    {
        public MyBlogDbContext()
        {
        }

        public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Floors> Floors { get; set; }
        public virtual DbSet<Forums> Forums { get; set; }
        public virtual DbSet<FriendLinks> FriendLinks { get; set; }
        public virtual DbSet<Labels> Labels { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<Moderator> Moderator { get; set; }
        public virtual DbSet<Options> Options { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<SetArtitleLabel> SetArtitleLabel { get; set; }
        public virtual DbSet<SetArtitleSort> SetArtitleSort { get; set; }
        public virtual DbSet<Sorts> Sorts { get; set; }
        public virtual DbSet<Submenus> Submenus { get; set; }
        public virtual DbSet<UserFriends> UserFriends { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=MyBlogDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articles>(entity =>
            {
                entity.HasKey(e => e.ArticleId)
                    .HasName("PK__camefor___CC36F6607C9BDA87");

                entity.ToTable("articles");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ArticleCommentCount)
                    .HasColumnName("article_comment_count")
                    .HasComment("评论总数");

                entity.Property(e => e.ArticleContent)
                    .IsRequired()
                    .HasColumnName("article_content")
                    .HasComment("博文内容");

                entity.Property(e => e.ArticleDate)
                    .HasColumnName("article_date")
                    .HasComment("发表日期");

                entity.Property(e => e.ArticleLikeCount)
                    .HasColumnName("article_like_count")
                    .HasComment("点赞数");

                entity.Property(e => e.ArticleTitle)
                    .IsRequired()
                    .HasColumnName("article_title")
                    .HasComment("博文标题");

                entity.Property(e => e.ArticleViews)
                    .HasColumnName("article_views")
                    .HasComment("浏览量");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("发表用户ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("camefor_articles_ibfk_1");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK__zj_comme__E79576874369A69D");

                entity.ToTable("comments");

                entity.HasComment("文章评论表");

                entity.HasIndex(e => e.ArticleId)
                    .HasName("article_id");

                entity.HasIndex(e => e.CommentDate)
                    .HasName("comment_date");

                entity.HasIndex(e => e.ParentCommentId)
                    .HasName("parent_comment_id");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .HasComment("文章ID");

                entity.Property(e => e.CommentContent)
                    .IsRequired()
                    .HasColumnName("comment_content")
                    .HasComment("评论内容");

                entity.Property(e => e.CommentDate)
                    .HasColumnName("comment_date")
                    .HasComment("评论日期");

                entity.Property(e => e.CommentLikeCount)
                    .HasColumnName("comment_like_count")
                    .HasComment("给评论点赞数");

                entity.Property(e => e.ParentCommentId)
                    .HasColumnName("parent_comment_id")
                    .HasComment("父评论ID");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("发表评论用户ID");
            });

            modelBuilder.Entity<Floors>(entity =>
            {
                entity.HasKey(e => e.FloorId)
                    .HasName("PK__zj_floor__76040CCC2BB7BB0E");

                entity.ToTable("floors");

                entity.HasComment("回帖表");

                entity.HasIndex(e => e.FloorDate)
                    .HasName("floor_date");

                entity.HasIndex(e => e.ParentFloorId)
                    .HasName("parent_floor_id");

                entity.HasIndex(e => new { e.UserId, e.PostId })
                    .HasName("user_id");

                entity.Property(e => e.FloorId)
                    .HasColumnName("floor_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.FloorContent)
                    .IsRequired()
                    .HasColumnName("floor_content")
                    .HasComment("回帖内容");

                entity.Property(e => e.FloorDate)
                    .HasColumnName("floor_date")
                    .HasComment("回帖时间");

                entity.Property(e => e.ParentFloorId)
                    .HasColumnName("parent_floor_id")
                    .HasComment("父回帖ID");

                entity.Property(e => e.PostId)
                    .HasColumnName("post_id")
                    .HasComment("所属主贴ID");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("回帖用户ID");
            });

            modelBuilder.Entity<Forums>(entity =>
            {
                entity.HasKey(e => e.ForumId)
                    .HasName("PK__zj_forum__69A2FA5879B5C8F7");

                entity.ToTable("forums");

                entity.HasIndex(e => e.ForumName)
                    .HasName("forum_name");

                entity.HasIndex(e => e.ParentForumId)
                    .HasName("parent_forum_id");

                entity.Property(e => e.ForumId)
                    .HasColumnName("forum_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ForumDescription)
                    .IsRequired()
                    .HasColumnName("forum_description");

                entity.Property(e => e.ForumLogo)
                    .IsRequired()
                    .HasColumnName("forum_logo")
                    .HasMaxLength(255);

                entity.Property(e => e.ForumName)
                    .IsRequired()
                    .HasColumnName("forum_name")
                    .HasMaxLength(20);

                entity.Property(e => e.ForumPostCount).HasColumnName("forum_post_count");

                entity.Property(e => e.ParentForumId).HasColumnName("parent_forum_id");
            });

            modelBuilder.Entity<FriendLinks>(entity =>
            {
                entity.HasKey(e => e.FriendLinkId)
                    .HasName("PK__zj_frien__531D65D53364558C");

                entity.ToTable("friend_links");

                entity.HasComment("友情链接");

                entity.Property(e => e.FriendLinkId)
                    .HasColumnName("friend_link_id")
                    .HasComment("友链ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FriendLinkDescription)
                    .IsRequired()
                    .HasColumnName("friend_link_description")
                    .HasComment("友情链接描述");

                entity.Property(e => e.FriendLinkLogo)
                    .IsRequired()
                    .HasColumnName("friend_link_logo")
                    .HasMaxLength(255)
                    .HasComment("友链Logo");

                entity.Property(e => e.FriendLinkName)
                    .IsRequired()
                    .HasColumnName("friend_link_name")
                    .HasMaxLength(20)
                    .HasComment("友情链接名称");

                entity.Property(e => e.FriendLinks1)
                    .IsRequired()
                    .HasColumnName("friend_links")
                    .HasMaxLength(255)
                    .HasComment("友情链接");
            });

            modelBuilder.Entity<Labels>(entity =>
            {
                entity.HasKey(e => e.LabelId)
                    .HasName("PK__zj_label__E44FFA5848117068");

                entity.ToTable("labels");

                entity.HasComment("标签表");

                entity.HasIndex(e => e.LabelAlias)
                    .HasName("label_alias");

                entity.HasIndex(e => e.LabelName)
                    .HasName("label_name");

                entity.Property(e => e.LabelId)
                    .HasColumnName("label_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.LabelAlias)
                    .IsRequired()
                    .HasColumnName("label_alias")
                    .HasMaxLength(15)
                    .HasComment("标签别名");

                entity.Property(e => e.LabelDescription)
                    .IsRequired()
                    .HasColumnName("label_description")
                    .HasComment("标签描述");

                entity.Property(e => e.LabelName)
                    .IsRequired()
                    .HasColumnName("label_name")
                    .HasMaxLength(20)
                    .HasComment("标签名");
            });

            modelBuilder.Entity<Menus>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK__zj_menus__4CA0FADC8B51BF1F");

                entity.ToTable("menus");

                entity.HasComment("总菜单表");

                entity.Property(e => e.MenuId)
                    .HasColumnName("menu_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasColumnName("menu_name")
                    .HasMaxLength(20)
                    .HasComment("菜单名");
            });

            modelBuilder.Entity<Moderator>(entity =>
            {
                entity.HasKey(e => new { e.ModeratorId, e.ForumId })
                    .HasName("PK__zj_moder__71CC7ADC81950142");

                entity.ToTable("moderator");

                entity.HasComment("版主表");

                entity.Property(e => e.ModeratorId)
                    .HasColumnName("moderator_id")
                    .HasComment("版主ID");

                entity.Property(e => e.ForumId)
                    .HasColumnName("forum_id")
                    .HasComment("论坛ID");

                entity.Property(e => e.ModeratorLevel)
                    .IsRequired()
                    .HasColumnName("moderator_level")
                    .HasMaxLength(20)
                    .HasComment("版主级别");
            });

            modelBuilder.Entity<Options>(entity =>
            {
                entity.HasKey(e => e.OptionId)
                    .HasName("PK__zj_optio__F4EACE1B80C1E841");

                entity.ToTable("options");

                entity.HasComment("网站管理");

                entity.HasIndex(e => e.OptionName)
                    .HasName("option_name");

                entity.Property(e => e.OptionId)
                    .HasColumnName("option_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.OptionName)
                    .IsRequired()
                    .HasColumnName("option_name")
                    .HasMaxLength(255);

                entity.Property(e => e.OptionValues)
                    .IsRequired()
                    .HasColumnName("option_values");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("PK__zj_posts__3ED7876682E3B23D");

                entity.ToTable("posts");

                entity.HasComment("论坛管理");

                entity.Property(e => e.PostId)
                    .HasColumnName("post_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ForumId)
                    .HasColumnName("forum_id")
                    .HasComment("论坛ID");

                entity.Property(e => e.PostCommentCount)
                    .HasColumnName("post_comment_count")
                    .HasComment("回帖个数");

                entity.Property(e => e.PostContent)
                    .IsRequired()
                    .HasColumnName("post_content")
                    .HasComment("帖子内容");

                entity.Property(e => e.PostDate)
                    .HasColumnName("post_date")
                    .HasComment("发帖时间");

                entity.Property(e => e.PostStatus)
                    .IsRequired()
                    .HasColumnName("post_status")
                    .HasMaxLength(20)
                    .HasComment("帖子状态");

                entity.Property(e => e.PostTitle)
                    .IsRequired()
                    .HasColumnName("post_title")
                    .HasComment("帖子标题");

                entity.Property(e => e.PostViews)
                    .HasColumnName("post_views")
                    .HasComment("帖子浏览量");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("回帖用户ID");
            });

            modelBuilder.Entity<SetArtitleLabel>(entity =>
            {
                entity.HasKey(e => e.ArticleId)
                    .HasName("PK__zj_set_a__CC36F660A7EE22DA");

                entity.ToTable("set_artitle_label");

                entity.HasIndex(e => e.LabelId)
                    .HasName("label_id");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.LabelId).HasColumnName("label_id");
            });

            modelBuilder.Entity<SetArtitleSort>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.SortId })
                    .HasName("PK__zj_set_a__6F0755802FB3CB17");

                entity.ToTable("set_artitle_sort");

                entity.HasComment("文章设置分类表");

                entity.HasIndex(e => e.SortId)
                    .HasName("sort_id");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_id")
                    .HasComment("文章ID");

                entity.Property(e => e.SortId)
                    .HasColumnName("sort_id")
                    .HasComment("分类ID");
            });

            modelBuilder.Entity<Sorts>(entity =>
            {
                entity.HasKey(e => e.SortId)
                    .HasName("PK__zj_sorts__331A3E0B0E7781E3");

                entity.ToTable("sorts");

                entity.HasComment("分类管理");

                entity.HasIndex(e => e.SortAlias)
                    .HasName("sort_alias");

                entity.HasIndex(e => e.SortName)
                    .HasName("sort_name");

                entity.Property(e => e.SortId)
                    .HasColumnName("sort_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ParentSortId)
                    .HasColumnName("parent_sort_id")
                    .HasComment("父分类");

                entity.Property(e => e.SortAlias)
                    .IsRequired()
                    .HasColumnName("sort_alias")
                    .HasMaxLength(15)
                    .HasComment("分类别名");

                entity.Property(e => e.SortDescription)
                    .IsRequired()
                    .HasColumnName("sort_description")
                    .HasComment("分类描述");

                entity.Property(e => e.SortName)
                    .IsRequired()
                    .HasColumnName("sort_name")
                    .HasMaxLength(50)
                    .HasComment("分类名称");
            });

            modelBuilder.Entity<Submenus>(entity =>
            {
                entity.HasKey(e => e.LinkId)
                    .HasName("PK__zj_subme__93B0078C107480EA");

                entity.ToTable("submenus");

                entity.HasComment("子菜单ID");

                entity.Property(e => e.LinkId)
                    .HasColumnName("link_id")
                    .HasComment("子菜单ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LinkName)
                    .IsRequired()
                    .HasColumnName("link_name")
                    .HasMaxLength(255)
                    .HasComment("子菜单名称");

                entity.Property(e => e.LinkOpenWay)
                    .IsRequired()
                    .HasColumnName("link_open_way")
                    .HasMaxLength(20)
                    .HasComment("子菜单打开方式");

                entity.Property(e => e.LinkTarget)
                    .IsRequired()
                    .HasColumnName("link_target")
                    .HasMaxLength(255)
                    .HasComment("子菜单链接");

                entity.Property(e => e.MenuId)
                    .HasColumnName("menu_id")
                    .HasComment("总菜单ID");

                entity.Property(e => e.ParentLinkId)
                    .HasColumnName("parent_link_id")
                    .HasComment("父菜单ID");
            });

            modelBuilder.Entity<UserFriends>(entity =>
            {
                entity.ToTable("user_friends");

                entity.HasComment("用户好友表");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserFriendsId)
                    .HasColumnName("user_friends_id")
                    .HasComment("好友ID");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户ID");

                entity.Property(e => e.UserNote)
                    .IsRequired()
                    .HasColumnName("user_note")
                    .HasMaxLength(20)
                    .HasComment("好友备注");

                entity.Property(e => e.UserStatus)
                    .IsRequired()
                    .HasColumnName("user_status")
                    .HasMaxLength(20)
                    .HasComment("好友状态");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__zj_users__B9BE370F53E6F405");

                entity.ToTable("users");

                entity.HasComment("用户管理");

                entity.HasIndex(e => e.UserEmail)
                    .HasName("user_email");

                entity.HasIndex(e => e.UserName)
                    .HasName("user_name");

                entity.HasIndex(e => e.UserNickname)
                    .HasName("user_nickname");

                entity.HasIndex(e => e.UserTelephoneNumber)
                    .HasName("user_telephone_number");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasComment("用户ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserAge).HasColumnName("user_age");

                entity.Property(e => e.UserBirthday)
                    .HasColumnName("user_birthday")
                    .HasColumnType("date");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasColumnName("user_email")
                    .HasMaxLength(30);

                entity.Property(e => e.UserIp)
                    .IsRequired()
                    .HasColumnName("user_ip")
                    .HasMaxLength(20)
                    .HasComment("用户IP");

                entity.Property(e => e.UserLevel)
                    .IsRequired()
                    .HasColumnName("user_level")
                    .HasMaxLength(20)
                    .HasComment("用户等级");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(20)
                    .HasComment("用户名");

                entity.Property(e => e.UserNickname)
                    .IsRequired()
                    .HasColumnName("user_nickname")
                    .HasMaxLength(20);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("user_password")
                    .HasMaxLength(15)
                    .HasComment("密码");

                entity.Property(e => e.UserProfilePhoto)
                    .IsRequired()
                    .HasColumnName("user_profile_photo")
                    .HasMaxLength(255)
                    .HasComment("用户头像");

                entity.Property(e => e.UserRegistrationTime).HasColumnName("user_registration_time");

                entity.Property(e => e.UserRights)
                    .IsRequired()
                    .HasColumnName("user_rights")
                    .HasMaxLength(20)
                    .HasComment("用户权限");

                entity.Property(e => e.UserTelephoneNumber).HasColumnName("user_telephone_number");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

/**
 * https://zhangjia.io/852.html
 * 
 * 1.  关系模式集合
网站管理（选项ID，选项名称，选项值）
用户（用户ID，用户IP，用户名，用户密码，用户邮箱，用户昵称，用户头像，用户等级，用户权限，注册时间，用户生日，用户年龄，用户手机号）
用户好友（标识ID，用户ID，好友ID，好友备注，好友状态）
博文（博文ID，发表用户ID，博文标题，博文内容，浏览量，评论总数，发表日期，点赞数）
评论（评论ID，发表用户ID，评论文章ID，点赞数，评论日期，评论内容，父评论ID）
分类（分类ID，分类名称，分类别名，分类描述，父分类ID）
博文设置分类（博文ID，分类ID）
标签（标签ID，标签名称，标签别名，标签描述）
博文设置标签（博文ID，标签ID）
总菜单（总菜单ID，总菜单名称）
子菜单（子菜单ID，菜单ID，子菜单名称，子菜单链接，子菜单打开方式，父菜单ID）
论坛（论坛ID，论坛名称，论坛描述，论坛Logo，论坛帖子个数，父论坛ID）
主贴（主题ID，论坛ID，发帖用户ID，帖子标题，帖子浏览量，帖子内容，发帖时间，帖子状态，回帖个数）
回复帖子（回帖ID，回帖用户ID，所属主贴ID，回帖内容，回帖时间，父回帖ID）
版主（版主ID，论坛ID，版主级别）
友情链接（友链ID，友链链接，友链名称，友链描述，友链Logo）
 * 
 * **/
