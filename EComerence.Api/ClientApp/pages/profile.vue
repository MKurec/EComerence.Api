<template>
  <section class="section">
    <div class="container">
      <h2 class="title">My Profile</h2>
      <div class="content">
        <p>
          <strong>Username:</strong>
          {{ loggedInUser.firstName }}
        </p>
        <p>
          <strong>Email:</strong>
          {{ loggedInUser.email }}
          {{$auth.loggedIn}}
        </p>
        <p>
          {{this.$auth.user}}

        </p>
      </div>
    </div>
  </section>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  computed: {
    ...mapGetters(['loggedInUser'])
  }
}
</script>