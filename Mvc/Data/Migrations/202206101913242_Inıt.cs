namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inıt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnaTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Alıcı = c.String(),
                        Gönderici = c.String(),
                        ProjeName = c.String(),
                        ProjectStatus = c.String(),
                        ProjectType = c.String(),
                        SponsorName = c.String(),
                        PatientSpecialization = c.String(),
                        CancerSpecies = c.String(),
                        MaterialType = c.String(),
                        MaterialQuantity = c.String(),
                        TubeType = c.String(),
                        Phase = c.String(),
                        Tarih = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMailTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HastaUzmanlık",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HastaUzmanlıkName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KanserTürleri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KanserTürüName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MateryalTipis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MateryalTipiName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sponsors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SponsorName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TüpCinsi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TüpCinsiName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SureName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        AdminRole = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppUsers");
            DropTable("dbo.TüpCinsi");
            DropTable("dbo.Sponsors");
            DropTable("dbo.MateryalTipis");
            DropTable("dbo.KanserTürleri");
            DropTable("dbo.HastaUzmanlık");
            DropTable("dbo.EMailTables");
            DropTable("dbo.AnaTables");
        }
    }
}
