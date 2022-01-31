const { RESTDataSource } = require('apollo-datasource-rest');
const { ApolloError } = require('apollo-server-errors');
const _ = require('lodash');

class PersonAPI extends RESTDataSource {
    constructor(){
        super();
        this.baseURL = 'https://localhost:44318/api/Data';
        //this.baseURL = 'https://alintapocapi.azurewebsites.net/api/Data';
    }

    async getPeople() {
      try {
        const data = await this.get(`/GetAllPeople`);
        return data;
      } catch(error) {
        return new ApolloError("Unable to get data from API", "API_ERROR");
      }
    }

    async getPersonById(id) {
      try{
        const data = await this.get(`/GetPersonById/${encodeURIComponent(id)}`);
        return data;
      } catch(error) {
        return new ApolloError("Unable to get data from API", "API_ERROR");
      }
    }

    async getPeopleFilter(args) {
      try{
        const data = await this.get(`/GetAllPeople`);
        return _.filter(data, args);
      } catch(error) {
        return new ApolloError("Unable to get data from API", "API_ERROR");
      }
    }

    async addPerson(person) {
      try{
        await this.post(`/AddPerson`, person);
      } catch(error) {
        return new ApolloError("Unable to get data from API", "API_ERROR");
      }
    }

    async updatePerson(person) {
      try{
        await this.put(`/UpdatePerson`, person);
      } catch(error) {
        return new ApolloError("Unable to get data from API", "API_ERROR");
      }
    }

    async deletePerson(id) {
      try{
        await this.delete(`/DeletePerson/${encodeURIComponent(id)}`);
      } catch(error) {
        return new ApolloError("Unable to get data from API", "API_ERROR");
      }
    }
}

module.exports = PersonAPI;