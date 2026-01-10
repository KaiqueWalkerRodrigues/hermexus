FROM nginx:alpine

# Remove the default configuration.
RUN rm /etc/nginx/conf.d/default.conf

# Copy your configuration to the correct location that Nginx expects.
COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
EXPOSE 443