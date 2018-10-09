using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoardGamesDirectory.Models;

namespace BoardGamesDirectory.Controllers
{
    public class GamesController : Controller
    {
        private BoardGamesDBMk3Entities db = new BoardGamesDBMk3Entities();

        // GET: Games
        public ActionResult Index(string GameSearch)
        {
            var games = from l in db.Games.Include(g => g.Category).Include(g => g.Publisher).Include(g => g.Speed)
            select l;


            if (!String.IsNullOrEmpty(GameSearch))
            {
                games = games.Where(x => x.GameName.Contains(GameSearch));
            }

            return View(games.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.PublisherId = new SelectList(db.Publishers, "PublisherId", "PublisherName");
            ViewBag.SpeedId = new SelectList(db.Speeds, "SpeedId", "SpeedName");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameId,GameName,GameDescription,Published,MaxPlayers,MinPlayers,MaxAge,MinAge,WebLink,CoverImg,GmplayImgOne,GmplayImgTwo,GmplayImgThree,PublisherId,CategoryId,SpeedId")] Game game)
        {
            if (game.CoverImg == null)
            {
                game.CoverImg = "https://filestore.community.support.microsoft.com/api/images/72e3f188-79a1-465f-90ca-27262d769841";
            }
            if (game.GmplayImgOne == null)
            {
                game.GmplayImgOne = "https://filestore.community.support.microsoft.com/api/images/72e3f188-79a1-465f-90ca-27262d769841";
            }
            if (game.GmplayImgTwo == null)
            {
                game.GmplayImgTwo = "https://filestore.community.support.microsoft.com/api/images/72e3f188-79a1-465f-90ca-27262d769841";
            }
            if (game.GmplayImgThree == null)
            {
                game.GmplayImgThree = "https://filestore.community.support.microsoft.com/api/images/72e3f188-79a1-465f-90ca-27262d769841";
            }

            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", game.CategoryId);
            ViewBag.PublisherId = new SelectList(db.Publishers, "PublisherId", "PublisherName", game.PublisherId);
            ViewBag.SpeedId = new SelectList(db.Speeds, "SpeedId", "SpeedName", game.SpeedId);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", game.CategoryId);
            ViewBag.PublisherId = new SelectList(db.Publishers, "PublisherId", "PublisherName", game.PublisherId);
            ViewBag.SpeedId = new SelectList(db.Speeds, "SpeedId", "SpeedName", game.SpeedId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,GameName,GameDescription,Published,MaxPlayers,MinPlayers,MaxAge,MinAge,WebLink,CoverImg,GmplayImgOne,GmplayImgTwo,GmplayImgThree,PublisherId,CategoryId,SpeedId")] Game game)
        {
            if (game.CoverImg == null)
            {
                game.CoverImg = "https://filestore.community.support.microsoft.com/api/images/72e3f188-79a1-465f-90ca-27262d769841";
            }
            if (game.GmplayImgOne == null)
            {
                game.GmplayImgOne = "https://filestore.community.support.microsoft.com/api/images/72e3f188-79a1-465f-90ca-27262d769841";
            }
            if (game.GmplayImgTwo == null)
            {
                game.GmplayImgTwo = "https://filestore.community.support.microsoft.com/api/images/72e3f188-79a1-465f-90ca-27262d769841";
            }
            if (game.GmplayImgThree == null)
            {
                game.GmplayImgThree = "https://filestore.community.support.microsoft.com/api/images/72e3f188-79a1-465f-90ca-27262d769841";
            }

            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", game.CategoryId);
            ViewBag.PublisherId = new SelectList(db.Publishers, "PublisherId", "PublisherName", game.PublisherId);
            ViewBag.SpeedId = new SelectList(db.Speeds, "SpeedId", "SpeedName", game.SpeedId);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
