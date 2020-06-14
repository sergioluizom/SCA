using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SCA.Infraestrutura;
using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using System.Linq;

namespace SCA.Repository.Implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Context context;
        public UsuarioRepository(Context context)
        {
            this.context = context;
        }

        public Usuario RecuperarUsuarioPorLoginSenha(string login, string password)
        {
            return context.Usuarios.Find(x => x.Login == login && x.Senha == password).FirstOrDefault();
        }

        public Usuario CriarUsuarioAdmin()
        {
            var usuario = new Usuario() { Perfil = "admin", Login = "admin", Senha = "admin" };
            context.Usuarios.InsertOne(usuario);
            return usuario;
        }
    }
}