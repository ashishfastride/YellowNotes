﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using YellowNotes.Api.Attributes;
using YellowNotes.Dto;

namespace YellowNotes.Api.Controllers
{
    [ValidateModelState]
    [RoutePrefix("api/good-notes")]
    public class GoodNotesController : NotesControllerBase
    {
        [HttpGet, Route("")]
        [ResponseType(typeof(IEnumerable<NoteDto>))]
        public IHttpActionResult Get()
        {
            return Ok(Notes.Values);
        }

        [HttpGet, Route("{id}", Name = "GetNote")]
        [ResponseType(typeof(NoteDto))]
        public IHttpActionResult GetNote(int id)
        {
            if (!Notes.ContainsKey(id))
            {
                return NotFound();
            }

            return Ok(Notes[id]);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Post([FromBody]NoteDto note)
        {
            int maxId = Notes.Keys.Max();
            note.Id = ++maxId;
            Notes.Add(maxId, note);

            return CreatedAtRoute("GetNote", new {id = note.Id}, note);
        }

        [HttpPut, Route("{id}")]
        public IHttpActionResult Put(int id, [FromBody]NoteDto note)
        {
            if (!Notes.ContainsKey(id))
            {
                return NotFound();
            }

            var dbNote = Notes[id];
            dbNote.Title = note.Title;
            dbNote.Content = note.Content;
            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (!Notes.ContainsKey(id))
            {
                return NotFound();
            }

            Notes.Remove(id);
            return Ok();
        }
    }
}