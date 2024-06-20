export default function ({ store, redirect }) {
  // If the user is not an admin
  debugger
  if (!store.$auth.$state.user || store.$auth.$state.user.role != "admin") {
    return redirect('/')
  }
}
