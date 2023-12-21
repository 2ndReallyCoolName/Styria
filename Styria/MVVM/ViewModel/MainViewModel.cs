using Styria.MVVM.Model.Song;
using Styria.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using Styria.MVVM.Model.Music;
using System.Collections.ObjectModel;

namespace Styria.MVVM.ViewModel
{
    class MainViewModel 
    {
        public Artist a1 = new Artist { Id = 1, Name = "Gabriele Motta" };
        public Song s1 = new Song { SongID = 1, Name = "Fairy Tail - Main Theme", ArtistID = 1};
        public Instrument i1 = new Instrument { Id = 1, Name = "Overdriven Guitar" };

        public Styria.MVVM.Model.Effect e1 = new Styria.MVVM.Model.Effect { ID=1, Name="Tie"};
        public Styria.MVVM.Model.Effect e2 = new Styria.MVVM.Model.Effect { ID=2, Name = "Slide" };
        public Styria.MVVM.Model.Effect e3 = new Styria.MVVM.Model.Effect { ID = 3, Name = "Legato Slide" };
        public Styria.MVVM.Model.Effect e4 = new Styria.MVVM.Model.Effect { ID = 4, Name = "Slide-in Below" };
        public Styria.MVVM.Model.Effect e5 = new Styria.MVVM.Model.Effect { ID = 5, Name = "Slide-in Above" };
        public Styria.MVVM.Model.Effect e6 = new Styria.MVVM.Model.Effect { ID = 6, Name = "Slide-out Upwards" };
        public Styria.MVVM.Model.Effect e7 = new Styria.MVVM.Model.Effect { ID = 7, Name = "Slide-out Downwards" };


        public Styria.MVVM.Model.Type t1 = new Styria.MVVM.Model.Type { Id = 1, Name = "Ghost Note" , TypeGroupID = 1};
        public Styria.MVVM.Model.Type t2 = new Styria.MVVM.Model.Type { Id = 2, Name = "Dead Note" , TypeGroupID = 1 };
        public Styria.MVVM.Model.Type t3 = new Styria.MVVM.Model.Type { Id = 3, Name = "Pinch Harmonic" , TypeGroupID = 2 };
        public Styria.MVVM.Model.Type t4 = new Styria.MVVM.Model.Type { Id = 4, Name = "Natural Harmonic" , TypeGroupID = 2 };
        public Styria.MVVM.Model.Type t5 = new Styria.MVVM.Model.Type { Id = 5, Name = "Palm Mute" , TypeGroupID = 4 };
        public Styria.MVVM.Model.Type t6 = new Styria.MVVM.Model.Type { Id = 6, Name = "Slight Vibrato" , TypeGroupID = 3 };
        public Styria.MVVM.Model.Type t7 = new Styria.MVVM.Model.Type { Id = 7, Name = "Wide Vibrato" , TypeGroupID = 3 };
        public Styria.MVVM.Model.Type t8 = new Styria.MVVM.Model.Type { Id = 8, Name = "Slight Vibrato w/ Trem Bar" , TypeGroupID = 3 };
        public Styria.MVVM.Model.Type t9 = new Styria.MVVM.Model.Type { Id = 9, Name = "Wide Vibrato w/ Trem Bar" , TypeGroupID = 3 };

        public Styria.MVVM.Model.TypeGroup tg1 = new Styria.MVVM.Model.TypeGroup { ID = 1, Name = "Notes" };
        public Styria.MVVM.Model.TypeGroup tg2 = new Styria.MVVM.Model.TypeGroup { ID = 2, Name = "Harmonics" };
        public Styria.MVVM.Model.TypeGroup tg3 = new Styria.MVVM.Model.TypeGroup { ID = 3, Name = "Vibratos" };
        public Styria.MVVM.Model.TypeGroup tg4 = new Styria.MVVM.Model.TypeGroup { ID = 4, Name = "Palm Mute" };


        public Styria.MVVM.Model.Note n1 = new Styria.MVVM.Model.Note { Id = 1, String = 'G', Fret = 7, InstrumentID = 1 , TypeID=9};
        public Styria.MVVM.Model.Note n2 = new Styria.MVVM.Model.Note { Id = 2, String = 'G', Fret = 9, InstrumentID = 1 };
        public Styria.MVVM.Model.Note n3 = new Styria.MVVM.Model.Note { Id = 3, String = 'G', Fret = 5, InstrumentID = 1, TypeID = 8 };
        public Styria.MVVM.Model.Note n4 = new Styria.MVVM.Model.Note { Id = 4, String = 'D', Fret = 7, InstrumentID = 1, TypeID = 4 };
        public Styria.MVVM.Model.Note n5 = new Styria.MVVM.Model.Note { Id = 5, String = 'D', Fret = 5, InstrumentID = 1 };
        public Styria.MVVM.Model.Note n6 = new Styria.MVVM.Model.Note { Id = 6, String = 'G', Fret = 10, InstrumentID = 1 };

        public Styria.MVVM.Model.TimeSignature ts1 = new TimeSignature { TimeSignatureID = 1, beats = 4, noteValue = 4 };

        public Styria.MVVM.Model.TabNote tbn1 = new TabNote { Id = 1, Duration = 8, Order=1, TabID = 1 , EffectID = 2};
        public Styria.MVVM.Model.TabNote tbn2 = new TabNote { Id = 2, Duration = 16, Order = 2, TabID = 1 , EffectID = 1};
        public Styria.MVVM.Model.TabNote tbn3 = new TabNote { Id = 3, Duration = 16, Order = 3, TabID = 1 };
        public Styria.MVVM.Model.TabNote tbn4 = new TabNote { Id = 4, Duration = 8, Order = 4, TabID = 1 };
        public Styria.MVVM.Model.TabNote tbn5 = new TabNote { Id = 5, Duration = 8, Order = 5, TabID = 1 , EffectID = 7};
        public Styria.MVVM.Model.TabNote tbn6 = new TabNote { Id = 6, Duration = 8, Order = 6, TabID = 1, EffectID = 5};
        public Styria.MVVM.Model.TabNote tbn7 = new TabNote { Id = 7, Duration = 16, Order = 7, TabID = 1 };
        public Styria.MVVM.Model.TabNote tbn8 = new TabNote { Id = 8, Duration = 16, Order = 8, TabID = 1 };
        public Styria.MVVM.Model.TabNote tbn9 = new TabNote { Id = 9, Duration = 8, Order = 9, TabID = 1 , EffectID = 6};
        public Styria.MVVM.Model.TabNote tbn10 = new TabNote { Id = 10, Duration = 8, Order = 10, TabID = 1 };
        public Styria.MVVM.Model.TabNote tbn11 = new TabNote { Id = 11, Duration = 8, Order = 11, TabID = 1 };



        public Styria.MVVM.Model.Tab tb1 = new Tab { ID = 1, InstrumentID = 1, TimeSignatureID = 1, SongID = 1 };
        public Styria.MVVM.Model.Tab tb2 = new Tab { ID = 2, InstrumentID = 1, TimeSignatureID = 1, SongID = 1 };

        public MainViewModel()
        {
            s1.Artist = a1;

            t1.TypeGroup = tg1; t2.TypeGroup = tg1; t3.TypeGroup = tg2; t4.TypeGroup = tg3; t5.TypeGroup = tg4;
            t6.TypeGroup = tg3; t7.TypeGroup = tg3; t8.TypeGroup = tg3; t9.TypeGroup = tg3;

            n1.Instrument = i1; n2.Instrument = i1; n3.Instrument = i1; n4.Instrument = i1; n5.Instrument = i1; n6.Instrument = i1;
            n1.Type = t8; n3.Type = t9; n4.Type = t4;


            tbn1.Notes = new Collection<Note> { n1 }; tbn1.Tab = tb1; tbn2.Notes = new Collection<Note> { n2 }; tbn2.Tab = tb1; 
            tbn3.Notes = new Collection<Note> { n1 }; tbn3.Tab = tb1; tbn4.Notes = new Collection<Note> { n3 }; tbn4.Tab = tb1;
            tbn5.Notes = new Collection<Note> { n4 }; tbn5.Tab = tb1; tbn6.Notes = new Collection<Note> { n5 }; tbn6.Tab = tb1; 
            tbn7.Notes = new Collection<Note> { n4 }; tbn7.Tab = tb1; tbn8.Notes = new Collection<Note> { n3 }; tbn8.Tab = tb1;
            tbn9.Notes = new Collection<Note> { n1 }; tbn9.Tab = tb1; tbn10.Notes = new Collection<Note> { n6 }; tbn10.Tab = tb1;
            tbn11.Notes = new Collection<Note> { n2 }; tbn11.Tab = tb1;

            tbn2.Effect = e1; tbn5.Effect = e6;
            tbn1.Effect = e2; tbn9.Effect = e7; tbn6.Effect = e5;

            tb1.TimeSignature = ts1; tb1.Instrument = i1; tb1.Song = s1;
            tb1.tabNotes = new Collection<TabNote> { tbn1, tbn2, tbn3, tbn4, tbn5, tbn6, tbn7, tbn8, tbn9, tbn4 };

            tb2.TimeSignature = ts1; tb2.Instrument = i1; tb2.Song = s1;
            tb2.tabNotes = new Collection<TabNote>{tbn1, tbn2, tbn3, tbn4, tbn5, tbn6, tbn7, tbn8, tbn10, tbn11};
        }

    }
}
