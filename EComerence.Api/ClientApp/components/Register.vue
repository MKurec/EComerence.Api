<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" persistent max-width="600px">
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="primary" dark v-bind="attrs" v-on="on">
          Załóż konto
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="headline">Załóż konto</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12" sm="6" md="6">
                <v-text-field v-model="register.lastName" label="Nazwisko" required></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="6">
                <v-text-field v-model="register.firstName" label="Imię" required></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field v-model="register.email" label="Email*" required></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  v-model="register.password"
                  :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                  :type="show1 ? 'text' : 'password'"
                  name="input-10-1"
                  label="Hasło"
                  hint="At least 8 characters"
                  counter
                  @click:append="show1 = !show1"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  v-model="checkPassword"
                  label="Hasło*"
                  type="password"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  v-model="register.address"
                  label="Adres"
                  type="password"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  v-model="register.city"
                  label="Miasto*"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  v-model="register.postalCode"
                  ref="zip"
                  label="Kod pocztowy"
                  required
                  placeholder="79-938"
                ></v-text-field>
              </v-col>
            </v-row>
          </v-container>
          <small>*indicates required field</small>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="dialog = false">
            Close
          </v-btn>
          <v-btn color="blue darken-1" text @click="registerUser">
            Register
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>
<script>
export default {
  data() {
    return {
      register: {
        firstName: "",
        lastName: "",
        email: "",
        password: "",
        city: "",
        address: "",
        postalCode: "",
      },
      show1: false,
      dialog: false,
      checkPassword: ""
    };
  },
  methods: {
    async registerUser() {
        await this.$axios.post('https://localhost:44367/Users/register', {
          firstName: this.register.firstName,
          lastName: this.register.lastName,
          email: this.register.email,
          password: this.register.password,
          city: this.register.city,
          address: this.register.address,
          postalCode: this.register.postalCode
        })
    
    }
  }
};
</script>