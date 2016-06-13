using AdminGujaratiSamaj.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminGujaratiSamaj.DAL
{
    public class UnitOfWork : IDisposable
    {
        private AGSDBContext context = new AGSDBContext();
        private MemberRepository memberRepository;
        private NonMemberEntryRepository nonMemberEntryRepository;

        #region Repo

        public MemberRepository MemberRepository
        {
            get
            {
                if (this.memberRepository == null)
                {
                    this.memberRepository = new MemberRepository(context);
                }
                return memberRepository;
            }
        }

        public NonMemberEntryRepository NonMemberEntryRepository
        {
            get
            {
                if (this.nonMemberEntryRepository == null)
                {
                    this.nonMemberEntryRepository = new NonMemberEntryRepository(context);
                }
                return nonMemberEntryRepository;
            }
        }
        #endregion

        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}