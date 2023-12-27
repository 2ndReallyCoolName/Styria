using Microsoft.EntityFrameworkCore;
using System;
using Styria.Model.Music;
using Type = Styria.Model.Music.Type;
using Styria.Model.Song;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Metrics;
using Instrument = Styria.Model.Song.Instrument;

namespace Styria.API.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
        {
        }

        public DbSet<Effect> Effects { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tab> Tabs { get; set; }
        public DbSet<TabNote> TabNotes { get; set; }
        public DbSet<TimeSignature> TimeSignatures { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<TypeGroup> TypeGroups { get; set; }


        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Instrument> Instruments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Artist
            modelBuilder.Entity<Artist>().HasData(new Artist { Id = 1, Name = "Gabriele Motta" });

            // Songs
            modelBuilder.Entity<Song>().HasData(new Song { SongID = 1, Name = "Fairy Tail - Main Theme", ArtistID = 1 });

            // Instrument
            modelBuilder.Entity<Instrument>().HasData(new Model.Song.Instrument { Id = 1, Name = "Overdriven Guitar" });

            // Effects
            //modelBuilder.Entity<Effect>().HasMany(e => e.TabNotes).WithOne(e => e.Effect).HasForeignKey(e => e.EffectID).IsRequired();

            modelBuilder.Entity<Effect>().HasData(new Effect { ID = 1, Name = "Tie" });
            modelBuilder.Entity<Effect>().HasData(new Effect { ID = 2, Name = "Slide" });
            modelBuilder.Entity<Effect>().HasData(new Effect { ID = 3, Name = "Legato Slide" });
            modelBuilder.Entity<Effect>().HasData(new Effect { ID = 4, Name = "Slide-in Below" });
            modelBuilder.Entity<Effect>().HasData(new Effect { ID = 5, Name = "Slide-in Above" });
            modelBuilder.Entity<Effect>().HasData(new Effect { ID = 6, Name = "Slide-out Upwards" });
            modelBuilder.Entity<Effect>().HasData(new Effect { ID = 7, Name = "Slide-out Downwards" });

            // Types
            //modelBuilder.Entity<Type>().HasOne(e => e.TypeGroup).WithMany(e => e.Types).HasForeignKey(e => e.TypeGroupID).IsRequired();
            //modelBuilder.Entity<Type>().HasMany(e => e.Notes).WithOne(e => e.Type).HasForeignKey(e => e.TypeID);

            modelBuilder.Entity<Type>().HasData(new Type { ID = 2, Name = "Ghost Note", TypeGroupID = 1 });
            modelBuilder.Entity<Type>().HasData(new Type { ID = 1, Name = "Dead Note", TypeGroupID = 1 });
            modelBuilder.Entity<Type>().HasData(new Type { ID = 3, Name = "Pinch Harmonic", TypeGroupID = 2 });
            modelBuilder.Entity<Type>().HasData(new Type { ID = 4, Name = "Natural Harmonic", TypeGroupID = 2 });
            modelBuilder.Entity<Type>().HasData(new Type { ID = 5, Name = "Palm Mute", TypeGroupID = 4 });
            modelBuilder.Entity<Type>().HasData(new Type { ID = 6, Name = "Slight Vibrato", TypeGroupID = 3 });
            modelBuilder.Entity<Type>().HasData(new Type { ID = 7, Name = "Wide Vibrato", TypeGroupID = 3 });
            modelBuilder.Entity<Type>().HasData(new Type { ID = 8, Name = "Slight Vibrato w/ Trem Bar", TypeGroupID = 3 });
            modelBuilder.Entity<Type>().HasData(new Type { ID = 9, Name = "Wide Vibrato w/ Trem Bar", TypeGroupID = 3 });

            // Type Groups
            //modelBuilder.Entity<TypeGroup>().HasMany(e => e.Types).WithOne(e => e.TypeGroup).HasForeignKey(e => e.TypeGroupID).IsRequired();


            modelBuilder.Entity<TypeGroup>().HasData(new TypeGroup { ID = 1, Name = "Notes" });
            modelBuilder.Entity<TypeGroup>().HasData(new TypeGroup { ID = 2, Name = "Harmonics" });
            modelBuilder.Entity<TypeGroup>().HasData(new TypeGroup { ID = 3, Name = "Vibratos" });
            modelBuilder.Entity<TypeGroup>().HasData(new TypeGroup { ID = 4, Name = "Palm Mute" });

            // Time Signature
            modelBuilder.Entity<TimeSignature>().HasData(new TimeSignature { TimeSignatureID = 1, Beats = 4, NoteValue = 4 });

            // Notes
            //modelBuilder.Entity<Note>().HasOne(e => e.Instrument).WithMany(e => e.Notes).HasForeignKey(e => e.InstrumentID).IsRequired();
            seedNotes(modelBuilder);

            // TabNotes
            var t1 = new TabNote { ID = 1, Duration = 8, Order = 1, TabID = 1, EffectID = 2};
            var t2 = new TabNote { ID = 2, Duration = 16, Order = 2, TabID = 1, EffectID = 1 };
            var t3 = new TabNote { ID = 3, Duration = 16, Order = 3, TabID = 1 };
            var t4 = new TabNote { ID = 4, Duration = 8, Order = 4, TabID = 1 };
            var t5 = new TabNote { ID = 5, Duration = 8, Order = 5, TabID = 1 };
            var t6 = new TabNote { ID = 6, Duration = 8, Order = 6, TabID = 1, EffectID = 7 };
            var t7 = new TabNote { ID = 7, Duration = 16, Order = 7, TabID = 1, EffectID = 5 };
            var t8 = new TabNote { ID = 8, Duration = 16, Order = 8, TabID = 1 };
            var t9 = new TabNote { ID = 9, Duration = 8, Order = 9, TabID = 1 };
            var t10 = new TabNote { ID = 10, Duration = 8, Order = 10, TabID = 1, EffectID = 6 };
            var t11 = new TabNote { ID = 11, Duration = 8, Order = 11, TabID = 1 };

            modelBuilder.Entity<TabNote>().HasMany(e => e.Notes).WithMany()
                .UsingEntity(nt => nt.HasData(
                    new { TabNoteID = 1, NotesID = 45 },
                    new { TabNoteID = 2, NotesID = 57 },
                    new { TabNoteID = 3, NotesID = 45 },
                    new { TabNoteID = 4, NotesID = 33 },
                    new { TabNoteID = 5, NotesID = 46 },
                    new { TabNoteID = 6, NotesID = 34 },
                    new { TabNoteID = 7, NotesID = 46 },
                    new { TabNoteID = 8, NotesID = 33 },
                    new { TabNoteID = 9, NotesID = 45 },
                    new { TabNoteID = 10, NotesID = 63 },
                    new { TabNoteID = 11, NotesID = 57 }));


            modelBuilder.Entity<TabNote>().HasData(t1);
            modelBuilder.Entity<TabNote>().HasData(t2);
            modelBuilder.Entity<TabNote>().HasData(t3);
            modelBuilder.Entity<TabNote>().HasData(t4);
            modelBuilder.Entity<TabNote>().HasData(t5);
            modelBuilder.Entity<TabNote>().HasData(t6);
            modelBuilder.Entity<TabNote>().HasData(t7);
            modelBuilder.Entity<TabNote>().HasData(t8);
            modelBuilder.Entity<TabNote>().HasData(t9);
            modelBuilder.Entity<TabNote>().HasData(t10);
            modelBuilder.Entity<TabNote>().HasData(t11);



            // Tabs
            modelBuilder.Entity<Tab>().HasData(new Tab { ID = 1, TimeSignatureID = 1, SongID = 1});
            modelBuilder.Entity<Tab>().HasData(new Tab { ID = 2, TimeSignatureID = 1, SongID = 1 });


        }

        private void seedNotes(ModelBuilder modelBuilder)
        {
            int _id = 1;
            IList<char> strings = new List<char>{'e', 'B', 'G', 'D', 'A', 'E'};

            // normal notes
            for (int i = 1; i <= 25; i++)
            {
                foreach (char s in strings)
                {
                    modelBuilder.Entity<Note>().HasData(new Note { ID = _id, String = s, Fret = i, InstrumentID = 1});
                    _id += 1;
                }
            }

            // notes with Types
            for (int i = 1; i <= 25;  i++)
            {
                foreach(char s in strings){
                    for(int t = 2;  t <= 9; t++) {
                        modelBuilder.Entity<Note>().HasData(new Note { ID = _id, String = s, Fret = i, InstrumentID = 1, TypeID = t});
                        _id += 1;
                    }
                }
            }

            // dead notes
            foreach (char s in strings)
            {
                modelBuilder.Entity<Note>().HasData(new Note { ID = _id, String = s, InstrumentID = 1, TypeID = 1 });
                _id += 1;
            }

        }
    }
}
