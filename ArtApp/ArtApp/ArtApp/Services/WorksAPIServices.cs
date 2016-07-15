using ArtApp.Models;
using System;
using System.Collections.Generic;

namespace ArtApp.Services
{
    public class WorksAPIServices
    {

        //Method to get the works from the API
        public List<Work> GetWorks()
        {
            //var list = new List<Work>();

            //for (int i = 0; i < 2; i++)
            //{
            //    list.Add(new Work
            //    {
            //        Title = "Noite Estrelada",
            //        Description = "Quadro pintado por Van Gogh",
            //        Date = DateTime.Parse("01-07-1889"),
            //        Heigth = 2,
            //        Width = 2,
            //        Length = 2,
            //        Technique = "Pintura a óleo",
            //        WorkId = 1,
            //        Classification = Classification.Good,
            //        Authors = new List<Author>
            //        {
            //            new Author
            //            {
            //                AuthorId = 1,
            //                Name = "Van Gogh",
            //            }
            //        }
            //    }
            //    );
            //}

            var list = new List<Work>
            {
                new Work
                {
                    Title = "Noite Estrelada",
                    Description = "Quadro pintado por Van Gogh",
                    Date = DateTime.Parse("01-07-1889"),
                    Heigth = 2,
                    Width = 2,
                    Length = 2,
                    Technique = "Pintura a óleo",
                    WorkId = 1,
                    Classification = Classification.Good,
                    Authors = new List<Author>
                    {
                        new Author
                        {
                            AuthorId = 1,
                            Name = "Van Gogh",
                        }
                    }

                },
                new Work
                {
                    Title = "Guernica",
                    Description = "Quadro pintado por Picasso",
                    Date = DateTime.Parse("01-07-1889"),
                    Heigth = 2,
                    Width = 2,
                    Length = 2,
                    Technique = "Pintura a óleo",
                    WorkId = 1,
                    Classification = Classification.Good,
                    Authors = new List<Author>
                    {
                        new Author
                        {
                            AuthorId = 1,
                            Name = "Picasso",
                        }
                    }
                }
            };



            return list;
        }

        //Method to edit the work
        public bool PutWork(int workId, Work work)
        {
            return true;
        }

        //Method to delete a work
        public bool DeleteWork(int workId)
        {
            return true;
        }
    }
}
